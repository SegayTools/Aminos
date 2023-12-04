using Aminos.Core.Services.Injections.Attrbutes;
using AminosUI.Services.Applications.Network;
using AminosUI.Services.ViewModelFactory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AminosUI.Services.Applications.Network.DefaultImpl
{
	[RegisterInjectable(typeof(IApplicationHttpFactory), ServiceLifetime.Singleton)]
	internal class DefaultApplicationHttpFactory : IApplicationHttpFactory
	{
		private HttpClient client;
		private readonly string baseUrl;
		private readonly IHttpClientFactory clientFactory;

		public DefaultApplicationHttpFactory(IHttpClientFactory clientFactory)
		{
			client = clientFactory.CreateClient();
			baseUrl = "https://localhost";
			this.clientFactory = clientFactory;
		}

		public void ResetAll()
		{
			client = clientFactory.CreateClient();
		}

		public async ValueTask<HttpResponseMessage> SendAsync(string url, Action<HttpRequestMessage> customizeRequestCallback = default, CancellationToken cancellation = default)
		{
			if (!url.StartsWith("http:"))
			{
				//relative url, combine with baseUrl
				if (url.FirstOrDefault() != '/')
					url = '/' + url;

				url = baseUrl + url;
			}

			var req = new HttpRequestMessage(HttpMethod.Get, url);
			customizeRequestCallback?.Invoke(req);

			var beginTime = DateTime.Now;
			try
			{
				var resp = await client.SendAsync(req, cancellation);
				return resp;
			}
			catch
			{
				throw;
			}
			finally
			{
				var endTime = DateTime.Now;
				var duration = endTime - beginTime;
			}
		}
	}
}
