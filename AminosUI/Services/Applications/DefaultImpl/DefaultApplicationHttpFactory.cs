using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Services.Injections.Attrbutes;
using AminosUI.Services.Applications.Network;
using Microsoft.Extensions.DependencyInjection;

namespace AminosUI.Services.Applications.DefaultImpl;

[RegisterInjectable(typeof(IApplicationHttpFactory), ServiceLifetime.Singleton)]
internal class DefaultApplicationHttpFactory : IApplicationHttpFactory
{
    private readonly string baseUrl;
    private HttpClient client;
    private HttpClientHandler handler = new ();

    public DefaultApplicationHttpFactory()
    {
        baseUrl = "https://localhost";
        client = CreateClient();
    }

    public void ResetAll()
    {
        client = CreateClient();
    }

    public CookieContainer Cookies
    {
        get => handler.CookieContainer;
        set => handler.CookieContainer = value;
    }

    public async ValueTask<HttpResponseMessage> SendAsync(string url,
        Action<HttpRequestMessage> customizeRequestCallback = default, CancellationToken cancellation = default)
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
        finally
        {
            var endTime = DateTime.Now;
            var duration = endTime - beginTime;
        }
    }

    private HttpClient CreateClient()
    {
        var http = new HttpClient(handler);
        return http;
    }
}