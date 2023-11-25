using Microsoft.AspNetCore.RequestDecompression;
using System.IO.Compression;

namespace Aminos.Utils
{
    public class ZlibOrGzipDecompressionProvider : IDecompressionProvider
    {
        private readonly IDecompressionProvider defaultStream;

        private class SeekableStream : Stream
        {
            private readonly Stream stream;
            public MemoryStream cachedStream = new MemoryStream();

            public SeekableStream(Stream stream)
            {
                this.stream = stream;
            }

            public override bool CanRead => stream.CanRead;

            public override bool CanSeek => false;

            public override bool CanWrite => false;

            public override long Length => stream.Length;

            public override long Position { get; set; }

            public override void Flush()
            {
                stream.Flush();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                if (Position < cachedStream.Length)
                {
                    cachedStream.Position = Position;
                    var cachedRead = cachedStream.Read(buffer, offset, count);
                    Position += cachedRead;
                    return cachedRead;
                }

                int read = stream.Read(buffer, offset, count);
                cachedStream.Write(buffer, offset, read);

                Position += read;
                return read;
            }

            public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            {
                if (Position < cachedStream.Length)
                {
                    cachedStream.Position = Position;
                    var cachedRead = await cachedStream.ReadAsync(buffer, offset, count, cancellationToken);
                    Position += cachedRead;
                    return cachedRead;
                }

                int read = await stream.ReadAsync(buffer, offset, count);
                cachedStream.Write(buffer, offset, read);

                Position += read;
                return read;
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return Position = offset + origin switch
                {
                    SeekOrigin.Begin => 0,
                    SeekOrigin.Current => Position,
                    SeekOrigin.End => Length,
                    _ => 0
                };
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }
        }

        public class ZlibOrGzipDecompressionStream : Stream
        {
            private readonly Stream stream;
            private readonly IDecompressionProvider defaultStreamProvider;
            private Stream actualDecompressionStream;

            public ZlibOrGzipDecompressionStream(Stream stream, IDecompressionProvider defaultStreamProvider)
            {
                this.stream = stream;
                this.defaultStreamProvider = defaultStreamProvider;
            }

            public override bool CanRead => stream.CanRead;

            public override bool CanSeek => stream.CanSeek;

            public override bool CanWrite => stream.CanWrite;

            public override long Length => stream.Length;

            public override long Position { get => stream.Position; set => stream.Position = value; }

            public override void Flush()
            {
                stream.Flush();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return stream.Read(buffer, offset, count);
            }

            public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            {
                if (actualDecompressionStream == null)
                {
                    await GenerateActualDecompressionStream(cancellationToken);
                }

                return await actualDecompressionStream.ReadAsync(buffer, offset, count, cancellationToken);
            }

            private async ValueTask GenerateActualDecompressionStream(CancellationToken cancellationToken)
            {
                var bytes = new byte[2];
                await stream.ReadExactlyAsync(bytes, cancellationToken);
                var isZlib = bytes[0] == 0x78 && bytes[1] switch
                {
                    0x01 or 0x5E or 0x9C or 0xDA or 0x20 or 0x7D or 0xBB or 0x79 => true,
                    _ => false
                };

                stream.Seek(0, SeekOrigin.Begin);
                actualDecompressionStream = isZlib ? new ZLibStream(stream, CompressionMode.Decompress) : defaultStreamProvider.GetDecompressionStream(stream);
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return stream.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                stream.SetLength(value);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                stream.Write(buffer, offset, count);
            }
        }

        public ZlibOrGzipDecompressionProvider(IDecompressionProvider defaultStream)
        {
            this.defaultStream = defaultStream;
        }

        public Stream GetDecompressionStream(Stream stream)
        {
            var seekable = new SeekableStream(stream);
            var actualStream = new ZlibOrGzipDecompressionStream(seekable, defaultStream);

            return actualStream;
        }
    }
}
