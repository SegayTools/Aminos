using System.Buffers;

namespace Aminos.Core.Utils.MethodExtensions
{
	public static class ArrayPoolMethodExtensions
	{
		public interface IBufferWithDisposable<T> : IDisposable
		{
			Memory<T> Memory { get; }
		}

		private class BufferWithDisposable<T> : IBufferWithDisposable<T>
		{
			private readonly ArrayPool<T> pool;
			private T[] rentBuf;
			private readonly int rentSize;

			public Memory<T> Memory => rentBuf.AsMemory().Slice(0, rentSize);

			public BufferWithDisposable(ArrayPool<T> pool, T[] rentBuf, int rentSize)
			{
				this.pool = pool;
				this.rentBuf = rentBuf;
				this.rentSize = rentSize;
			}

			public void Dispose()
			{
				if (rentBuf == null)
					return;
				pool.Return(rentBuf);
				rentBuf = default;
			}
		}

		public static IBufferWithDisposable<T> RentWithDisposable<T>(this ArrayPool<T> pool, int rentSize)
		{
			var rentBuf = pool.Rent(rentSize);
			var rent = new BufferWithDisposable<T>(pool, rentBuf, rentSize);
			return rent;
		}
	}
}
