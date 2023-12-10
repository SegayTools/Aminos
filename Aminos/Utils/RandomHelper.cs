namespace Aminos.Utils
{
	public class RandomHelper
	{
		private static Random random = new Random();

		public const string Digits = "0123456789";
		public const string UpperLetter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public const string LowerLetter = "abcdefghijklmnopqrstuvwxyz";
		public const string LowerHex = "0123456789abcdef";
		public const string UpperHex = "0123456789ABCDEF";

		public const string LettersAndDigits = Digits + UpperLetter + LowerLetter;

		public static string RandomString(string chars, int len)
		{
			return string.Join(chars, Enumerable.Repeat(0, len).Select(x => chars[random.Next(chars.Length)]));
		}
	}
}
