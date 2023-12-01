using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AminosUI.Services.Applications.Network
{
	public interface IApplicationHttpFactory
	{
		ValueTask<HttpResponseMessage> SendAsync(string fullUrlOrPart, Action<HttpRequestMessage> customizeRequestCallback = default);
	}
}
