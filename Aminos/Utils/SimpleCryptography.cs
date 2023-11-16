using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Security.Cryptography;
using System.Text;

namespace Aminos.Utils
{
	public static class SimpleCryptography
	{
		private static readonly AesGcm aesGcm;

		static SimpleCryptography()
		{
			aesGcm = new AesGcm(Encoding.UTF8.GetBytes("阿米诺斯Aminos"));
		}

		public static string Encrypt(string plainText)
		{
			byte[] iv = new byte[12];
			RandomNumberGenerator.Fill(iv);

			var ciphertext = new byte[plainText.Length];
			aesGcm.Encrypt(iv, Encoding.UTF8.GetBytes(plainText), ciphertext, null);

			byte[] result = new byte[iv.Length + ciphertext.Length];
			Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
			Buffer.BlockCopy(ciphertext, 0, result, iv.Length, ciphertext.Length);

			return Convert.ToBase64String(result);
		}

		public static string Decrypt(string cipherText)
		{
			byte[] data = Convert.FromBase64String(cipherText);

			byte[] iv = new byte[12];
			Buffer.BlockCopy(data, 0, iv, 0, iv.Length);

			byte[] ciphertext = new byte[data.Length - iv.Length];
			Buffer.BlockCopy(data, iv.Length, ciphertext, 0, ciphertext.Length);

			byte[] decryptedData = new byte[ciphertext.Length];
			aesGcm.Decrypt(iv, ciphertext, decryptedData, null);

			return Encoding.UTF8.GetString(decryptedData);
		}
	}
}
