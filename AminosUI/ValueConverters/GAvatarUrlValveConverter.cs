using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AminosUI.ValueConverters
{
	public class GAvatarUrlValveConverter : IValueConverter
	{
		SHA256 sha256 = SHA256.Create();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var email = value?.ToString() ?? string.Empty;
			var hash = System.Convert.ToHexString(sha256.ComputeHash(Encoding.UTF8.GetBytes(email.Trim().ToLowerInvariant()))).ToLowerInvariant();

			var size = parameter switch
			{
				int s => s,
				string str => int.Parse(str),
				_ => 128
			};

			var url = $"https://gravatar.com/avatar/{hash}?size={size}";
			return url;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
