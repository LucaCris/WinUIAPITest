using APIModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinUIAPIApp;

internal static class HttpUtils
{
    const string APIURL = "http://localhost:8192";
    public static HttpClient HttpClient { get; } = new HttpClient();    // HttpClient is intended to be instantiated once and reused throughout the life of an application

    public async static Task<TResult> Post<TResult>(this object data, string url, string api, CancellationToken cancellationToken = default)
    {
        try {
            var post = await HttpClient.PostAsJsonAsync(new Uri(new Uri(url), api), data, cancellationToken: cancellationToken);
            post.EnsureSuccessStatusCode();
            return await post.Content.ReadFromJsonAsync<TResult>(cancellationToken: cancellationToken);
        }
        catch (Exception ex) {
            var res = Activator.CreateInstance<TResult>();
            if (res is BaseResponse resp) {
                resp.Message = (ex is TaskCanceledException && cancellationToken.IsCancellationRequested) ? null : ex.Message;  // ignora errore se il 'cancel' arriva dall'utente (mostra invece quello da TimeOut)
                resp.Error = true;
            }
            return res;
        }
    }

    public async static Task<TResult> PostAPI<TResult>(this object data, string api, CancellationToken cancellationToken = default)
        => await Post<TResult>(data, APIURL, api, cancellationToken);

    public async static Task<HttpResponseMessage> PostAPI(this object data, string api, CancellationToken cancellationToken = default)
        => await HttpClient.PostAsJsonAsync(new Uri(new Uri(APIURL), api), data, cancellationToken: cancellationToken);
}
