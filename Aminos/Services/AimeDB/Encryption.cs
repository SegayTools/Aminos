using System.Buffers;
using System.Security.Cryptography;
using System.Text;

namespace Aminos.Services.AimeDB
{
	public static class Encryption
	{
		private static readonly byte[] KeyBytes = Encoding.UTF8.GetBytes("Copyright(C)SEGA");
		private static readonly Aes aesAlg;
		private static ICryptoTransform decryptor;
		private static ICryptoTransform encryptor;

		static Encryption()
		{
			aesAlg = Aes.Create();
			aesAlg.Key = KeyBytes;
			aesAlg.Mode = CipherMode.ECB;
			aesAlg.Padding = PaddingMode.None;

			decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
			encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
		}

		public static void Decrypt(in Span<byte> encryptBytes, Span<byte> outputDecryptBytes)
		{
			if (encryptBytes.Length != outputDecryptBytes.Length)
				throw new Exception($"outputDecryptBytes.Length({outputDecryptBytes.Length}) != encryptBytes.Length({encryptBytes.Length})");

			using var msEncrypt = new MemoryStream();
			msEncrypt.Write(encryptBytes);
			msEncrypt.Flush();
			msEncrypt.Seek(0, SeekOrigin.Begin);

			using var csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read);
			csEncrypt.Read(outputDecryptBytes);
		}

		public static void Encrypt(Span<byte> plainBytes, Span<byte> encryptBytes)
		{
			if (encryptBytes.Length != plainBytes.Length)
				throw new Exception($"encryptBytes.Length({encryptBytes.Length}) != plainBytes.Length({plainBytes.Length})");

			var buffer = ArrayPool<byte>.Shared.Rent(plainBytes.Length);

			using (var msEncrypt = new MemoryStream(buffer))
			{
				using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
				{
					csEncrypt.Write(plainBytes);
					csEncrypt.FlushFinalBlock();
				}
			}

			buffer[..plainBytes.Length].CopyTo(encryptBytes);
			ArrayPool<byte>.Shared.Return(buffer);
		}
	}
}
