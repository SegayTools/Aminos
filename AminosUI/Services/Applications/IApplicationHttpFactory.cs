using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AminosUI.Services.Applications.Network
{
	public interface IApplicationHttpFactory
	{
		CookieContainer Cookies { get; set; }
		
		ValueTask<HttpResponseMessage> SendAsync(string fullUrlOrPart, Action<HttpRequestMessage> customizeRequestCallback = default, CancellationToken cancellation = default);
		void ResetAll();
	}
}
