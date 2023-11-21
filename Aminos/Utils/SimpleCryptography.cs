using Aminos.Utils.MethodExtensions;
using System.Security.Cryptography;
using System.Text;

namespace Aminos.Utils
{
	public static class SimpleCryptography
	{
		private static readonly AesGcm aesGcm;

		static SimpleCryptography()
		{
			var key = new byte[24];
			key.AsSpan().WriteValue(0, Encoding.UTF8.GetBytes("阿米诺斯Aminos"));
			for (int i = 19; i < key.Length; i++)
				key[i] = (byte)i;
			aesGcm = new AesGcm(key);
		}

		public static string Encrypt(string plainText)
		{
			byte[] iv = new byte[12];
			RandomNumberGenerator.Fill(iv);

			var rawText = Encoding.UTF8.GetBytes(plainText);
			var ciphertext = new byte[rawText.Length];
			var tag = new byte[AesGcm.TagByteSizes.MinSize];
			aesGcm.Encrypt(iv, rawText, ciphertext, tag);

			byte[] result = new byte[iv.Length + ciphertext.Length + tag.Length];
			Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
			Buffer.BlockCopy(tag, 0, result, iv.Length, tag.Length);
			Buffer.BlockCopy(ciphertext, 0, result, iv.Length + tag.Length, ciphertext.Length);

			return Convert.ToHexString(result);
		}

		public static string Decrypt(string cipherText)
		{
			byte[] data = Convert.FromHexString(cipherText);

			byte[] iv = new byte[12];
			Buffer.BlockCopy(data, 0, iv, 0, iv.Length);

			byte[] tag = new byte[AesGcm.TagByteSizes.MinSize];
			Buffer.BlockCopy(data, iv.Length, tag, 0, tag.Length);

			byte[] ciphertext = new byte[data.Length - iv.Length - tag.Length];
			Buffer.BlockCopy(data, iv.Length + tag.Length, ciphertext, 0, ciphertext.Length);

			byte[] decryptedData = new byte[ciphertext.Length];
			aesGcm.Decrypt(iv, ciphertext, tag, decryptedData);

			return Encoding.UTF8.GetString(decryptedData);
		}
	}
}
