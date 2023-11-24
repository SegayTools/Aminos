using Aminos.Databases;
using Aminos.Databases.Title.SDEZ;
using Aminos.Handlers.AllNet;
using Aminos.Handlers.AllNet.Default;
using Aminos.Services.AimeDB;
using Aminos.Services.Injections;
using Aminos.Utils.MethodExtensions;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.RequestDecompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO.Compression;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Aminos
{
    public class Program
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
					var isZlib = bytes[0] == 0x78 && (bytes[1] switch
					{
						0x01 or 0x5E or 0x9C or 0xDA or 0x20 or 0x7D or 0xBB or 0x79 => true,
						_ => false
					});

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

		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			using var p12FileStream = typeof(Program).Assembly.GetManifestResourceStream("Aminos.Resources.Billing.server.p12");
			var p12FileBytes = await p12FileStream.ToByteArrayAsync();

			builder.WebHost.ConfigureKestrel(option =>
			{
				option.ListenAnyIP(8443, config =>
				{
					var password = "aquaserver";

					// ¼ÓÔØ .p12 Ö¤Êé
					var certificate = new X509Certificate2(p12FileBytes, password);
					config.UseHttps(certificate, conf =>
					{
						conf.SslProtocols = SslProtocols.Tls11;
					});
					config.Protocols = HttpProtocols.Http1;
				});

				option.ListenAnyIP(443, config =>
				{
					config.UseHttps();
				});

				option.ListenAnyIP(80, config =>
				{

				});
			});

			var fileProviderRootPath = Path.Combine(Path.GetTempPath(), "AnimosTemps");
			Directory.CreateDirectory(fileProviderRootPath);
			builder.Services.AddScoped<IFileProvider>((x) => new PhysicalFileProvider(fileProviderRootPath));

			// Add services to the container.
			builder.Services.AddControllers();
			builder.Services.AddSingleton<IAllNetHandler, DefaultAllNetHandler>();

			builder.Services.AddW3CLogging(o =>
			{
				o.LoggingFields = W3CLoggingFields.All;
			});
			builder.Services.AddHttpLogging(o =>
			{
				o.LoggingFields =
					HttpLoggingFields.RequestMethod |
					HttpLoggingFields.RequestBody |
					HttpLoggingFields.RequestHeaders |
					HttpLoggingFields.RequestProtocol |
					HttpLoggingFields.RequestScheme |
					HttpLoggingFields.RequestPath |
					HttpLoggingFields.RequestQuery;

				o.LoggingFields = o.LoggingFields |
					HttpLoggingFields.ResponseHeaders |
					HttpLoggingFields.ResponseStatusCode |
					HttpLoggingFields.ResponseBody;
			});

			builder.Services.AddDbContext<AminosDB>(options =>
				options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")),
			ServiceLifetime.Scoped);
			builder.Services.AddDbContext<MaimaiDXDB>(options =>
				options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")),
			ServiceLifetime.Scoped);

			builder.Services.AddLogging(options =>
			{
				options.AddSeq("http://localhost:5341/", "wSkYZUo2uMdeChLfusYI");
			});

			builder.Services.AddInjectsByAttributes(typeof(Program).Assembly);
			builder.Services.AddHostedService<AimeDBService>();
			builder.Services.AddRequestDecompression(option =>
			{
				var defaultDeflateProvider = option.DecompressionProviders["deflate"];
				option.DecompressionProviders["deflate"] = new ZlibOrGzipDecompressionProvider(defaultDeflateProvider);
			});

			var app = builder.Build();

			await CheckDBMigrations<AminosDB>(app.Services);
			await CheckDBMigrations<MaimaiDXDB>(app.Services);

			//app.UseHttpsRedirection();
			app.UseAuthorization();
			app.UseRequestDecompression();
			app.UseW3CLogging();
			app.UseHttpLogging();
			app.MapControllers();

			await app.RunAsync();
		}

		private static async ValueTask CheckDBMigrations<T>(IServiceProvider provider) where T : DbContext
		{
			var loggerFactory = provider.GetService<ILoggerFactory>();
			var logger = loggerFactory.CreateLogger(typeof(T));

			using var scope = provider.CreateScope();
			using var db = scope.ServiceProvider.GetRequiredService<T>();

			var applieds = (await db.Database.GetAppliedMigrationsAsync()).ToArray();
			var pendings = (await db.Database.GetPendingMigrationsAsync()).ToArray();

			logger.LogInformation($"Applied migrations:");
			foreach (var item in applieds)
				logger.LogInformation($" * {item}");
			logger.LogInformation($"Pending migrations:");
			foreach (var item in pendings)
				logger.LogInformation($" * {item}");

			await db.Database.MigrateAsync();
		}
	}
}
