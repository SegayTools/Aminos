using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Aminos.Core.Models.General;
using AminosUI.Services.Applications.Network;

namespace AminosUI.Utils.MethodExtensions;

public static class IHttpFactoryEx
{
    private static string GenerateQueryPath(object data)
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

        return string.Join("&", queryStringParameters);
    }

    public static async ValueTask<RESP> GetJson<RESP>(this IApplicationHttpFactory http, string url, object body,
        CancellationToken cancellation = default)
    {
        var resp = await http.SendAsync(url, req =>
        {
            req.Method = HttpMethod.Get;

            req.Content = JsonContent.Create(body);
        }, cancellation);

        var str = await resp.Content.ReadAsStringAsync(cancellation);
        return JsonSerializer.Deserialize<RESP>(str);
    }

    public static async ValueTask<T> Send<T>(this IApplicationHttpFactory http,
        string url, HttpMethod method,
        object formDataObj, object queryDataObj, CancellationToken cancellation,
        Func<Exception, T> fallbackValueIfException,
        Func<HttpResponseMessage, T> fallbackValueIfFailed)
    {
        var fixedUrl = url;
        if (queryDataObj is not null)
        {
            var queryPath = GenerateQueryPath(queryDataObj);
            if (fixedUrl[^1] != '?')
                fixedUrl += '?';
            fixedUrl += queryPath;
        }

        try
        {
            var resp = await http.SendAsync(fixedUrl, req =>
            {
                req.Method = method;

                if (formDataObj is not null)
                {
                    var form = new FormUrlEncodedContent(formDataObj.GetType().GetProperties()
                        .ToDictionary(x => x.Name, x => x.GetValue(formDataObj)?.ToString() ?? string.Empty));
                    req.Content = form;
                }
            }, cancellation);

            if (resp.StatusCode != HttpStatusCode.OK)
            {
                if (fallbackValueIfFailed is null)
                    throw new Exception($"url:{url} return status code {(int) resp.StatusCode}");
                return fallbackValueIfFailed.Invoke(resp);
            }

            var str = await resp.Content.ReadAsStringAsync(cancellation);
            return JsonSerializer.Deserialize<T>(str);
        }
        catch (Exception e)
        {
            if (fallbackValueIfException is null)
                throw;
            return fallbackValueIfException.Invoke(e);
            /*return ;*/
        }
    }

    public static ValueTask<CommonApiResponse> PostAsCommonApi(this IApplicationHttpFactory http, string url,
        object formDataObj = default, CancellationToken cancellation = default)
    {
        return Send(
            http,
            url,
            HttpMethod.Post,
            formDataObj,
            default,
            cancellation,
            e => new CommonApiResponse(false, $"内部异常:{e.Message}"),
            resp => new CommonApiResponse(false, $"请求StatusCode={(int) resp.StatusCode}({resp.StatusCode})"));
    }

    public static ValueTask<CommonApiResponse<T>> PostAsCommonApi<T>(this IApplicationHttpFactory http, string url,
        object formDataObj = default, CancellationToken cancellation = default)
    {
        return Send(
            http,
            url,
            HttpMethod.Post,
            formDataObj,
            default,
            cancellation,
            e => new CommonApiResponse<T>(false, default, $"内部异常:{e.Message}"),
            resp => new CommonApiResponse<T>(false, default,
                $"请求StatusCode={(int) resp.StatusCode}({resp.StatusCode})"));
    }

    public static ValueTask<CommonApiResponse> GetAsCommonApi(this IApplicationHttpFactory http, string url,
        object queryDataObj = default, CancellationToken cancellation = default)
    {
        return Send(
            http,
            url,
            HttpMethod.Get,
            default,
            queryDataObj,
            cancellation,
            e => new CommonApiResponse(false, $"内部异常:{e.Message}"),
            resp => new CommonApiResponse(false, $"请求StatusCode={(int) resp.StatusCode}({resp.StatusCode})"));
    }

    public static ValueTask<CommonApiResponse<T>> GetAsCommonApi<T>(this IApplicationHttpFactory http, string url,
        object queryDataObj = default, CancellationToken cancellation = default)
    {
        return Send(
            http,
            url,
            HttpMethod.Get,
            default,
            queryDataObj,
            cancellation,
            e => new CommonApiResponse<T>(false, default, $"内部异常:{e.Message}"),
            resp => new CommonApiResponse<T>(false, default,
                $"请求StatusCode={(int) resp.StatusCode}({resp.StatusCode})"));
    }
}