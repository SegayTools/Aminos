using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aminos.Core.Services.Injections.Attrbutes;
using AminosUI.Services.Applications.Network;
using AminosUI.Services.Caches;

namespace AminosUI.Services.Applications.DefaultImpl;

[RegisterInjectable(typeof(IImageLoader))]
public class DefaultImageLoader : IImageLoader
{
    private const int ParallelCount = 2;
    private readonly ICacheManager cache;
    private readonly ConcurrentDictionary<string, WeakReference<byte[]>> cacheMap = new();
    private readonly IApplicationHttpFactory httpFactory;
    private readonly ConcurrentStack<LoadTask> tasks = new();
    private volatile bool isUpdating;

    public DefaultImageLoader(IApplicationHttpFactory httpFactory, ICacheManager cache)
    {
        this.httpFactory = httpFactory;
        this.cache = cache;

        new Thread(PrcessQueue)
        {
            Name = "DefaultImageLoader.PrcessQueue()",
            IsBackground = true
        }.Start();
    }

    public Task<byte[]> LoadImage(string url, CancellationToken cancellationToken)
    {
        var taskCompleteSource = new TaskCompletionSource<byte[]>();
        tasks.Push(new LoadTask(taskCompleteSource, url));
        return taskCompleteSource.Task;
    }

    private async void PrcessQueue()
    {
        var currentRunningTasks = new List<Task>(ParallelCount);
        while (true)
        {
            for (var i = 0; i < ParallelCount; i++)
                if (tasks.TryPop(out var task))
                    currentRunningTasks.Add(Task.Run(() =>
                    {
                        var url = task.url;
                        var taskSource = task.TaskSource;

                        return ProcessTask(url, taskSource);
                    }));

            if (currentRunningTasks.Count > 0)
                await Task.WhenAll(currentRunningTasks);
            currentRunningTasks.Clear();
            await Task.Delay(0);
        }
    }

    private async ValueTask ProcessTask(string url, TaskCompletionSource<byte[]> taskSource)
    {
        using var md5 = MD5.Create();
        var hash = Convert.ToHexString(md5.ComputeHash(Encoding.UTF8.GetBytes(url)));

        var data = await LoadFromInMemory(url);
        if (data != null)
        {
            taskSource.SetResult(data);
            return;
        }

        data = await cache.LoadCache(hash);
        if (data != null)
        {
            taskSource.SetResult(data);
            return;
        }

        data = await LoadFromNetwork(url);
        if (data == null)
        {
            taskSource.SetResult(null);
            return;
        }

        taskSource.SetResult(data);

        await cache.SaveCache(hash, data);
        await SaveFromInMemory(hash, data);
    }

    private async ValueTask SaveFromInMemory(string hash, byte[] data)
    {
        cacheMap[hash] = new WeakReference<byte[]>(data);
    }

    private async ValueTask<byte[]> LoadFromInMemory(string hash)
    {
        if (cacheMap.TryGetValue(hash, out var weakReference))
            if (weakReference.TryGetTarget(out var data))
                return data;

        return default;
    }

    private async ValueTask<byte[]> LoadFromNetwork(string url)
    {
        try
        {
            var resp = await httpFactory.SendAsync(url, req => { req.Method = HttpMethod.Get; });
            return await resp.Content.ReadAsByteArrayAsync();
        }
        catch (Exception e)
        {
            //todo log it
            return default;
        }
    }

    private record LoadTask(TaskCompletionSource<byte[]> TaskSource, string url);
}