using AminosUI.Services.Applications.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace AminosUI.Utils.MethodExtensions
{
	public static class IHttpFactoryEx
	{
		public static async ValueTask<T> Post<T>(this IApplicationHttpFactory http, string url, object formDataObj = null)
		{
			var resp = await http.SendAsync(url, req =>
			{
				req.Method = HttpMethod.Post;

				if (formDataObj is not null)
				{
					var form = new FormUrlEncodedContent(formDataObj.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(formDataObj)?.ToString() ?? string.Empty));
					req.Content = form;
				}
			});

			var str = await resp.Content.ReadAsStringAsync();

			return JsonSerializer.Deserialize<T>(str);
		}

		public static async ValueTask<T> Get<T>(this IApplicationHttpFactory http, string url, object queryDataObj = null)
		{
			string genQueryPath(object data)
			{
				var queryStringParameters = new List<string>();

				var properties = data.GetType().GetProperties();

				foreach (var property in properties)
				{
					var value = property.GetValue(data);
					if (value != null)
					{
						var encodedValue = HttpUtility.UrlEncode(value.ToString());
						queryStringParameters.Add($"{property.Name}={encodedValue}");
					}
				}

				// 使用 "?" 连接属性名和属性值，使用 "&" 分隔不同的参数
				return "?" + string.Join("&", queryStringParameters);
			}

			var resp = await http.SendAsync(url, req =>
			{
				req.Method = HttpMethod.Get;

				if (queryDataObj is not null)
				{
					var queryPath = genQueryPath(queryDataObj);
				}
			});

			var str = await resp.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<T>(str);
		}
	}
}
