using System.Runtime.InteropServices;

namespace Aminos.Core.Utils.MethodExtensions
{
	public static class MemorySpanMethodExtensions
	{
		public static void WriteValue<T>(this Memory<byte> memory, int offset, T value) where T : struct
			=> memory.Span.WriteValue(offset, value);

		public static void WriteValue(this Memory<byte> memory, int offset, ReadOnlySpan<byte> bytes)
			=> memory.Span.WriteValue(offset, bytes);

		public static void ClearValues<T>(this Memory<T> memory, T cleanValue = default)
			=> memory.Span.ClearValues(cleanValue);

		public static void WriteValue<T>(this Span<byte> memory, int offset, T value) where T : struct
		{
			if (memory.Length < offset + Marshal.SizeOf<T>())
				throw new ArgumentOutOfRangeException("Offset is out of range.");

			MemoryMarshal.Write(memory.Slice(offset), ref value);
		}

		public static void WriteValue(this Span<byte> memory, int offset, ReadOnlySpan<byte> bytes)
		{
			if (memory.Length < offset + bytes.Length)
				throw new ArgumentOutOfRangeException("Offset is out of range.");
			bytes.CopyTo(memory.Slice(offset));
		}

		public static void ClearValues<T>(this Span<T> memory, T cleanValue = default)
		{
			foreach (ref var item in memory)
				item = cleanValue;
		}
	}
}
