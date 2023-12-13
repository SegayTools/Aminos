using System.IO;
using System.Threading.Tasks;
using Aminos.Core.Services.Injections.Attrbutes;
using AminosUI.Services.Caches;

namespace AminosUI.Desktop.ServicesImpl.Caches;

[RegisterInjectable(typeof(ICacheManager))]
public class TempFileCacheManager : ICacheManager
{
    private readonly string folderPath;

    public TempFileCacheManager()
    {
        folderPath = Path.Combine(Path.GetTempPath(), "AminosUI.Desktop");
        Directory.CreateDirectory(folderPath);
    }
    
    public async ValueTask SaveCache(string hash, byte[] data)
    {
        var filePath = GetCacheFilePath(hash);

        using var fileStream = File.OpenWrite(filePath);
        await fileStream.WriteAsync(data, 0, data.Length);
    }

    public async ValueTask<byte[]> LoadCache(string hash)
    {
        var filePath = GetCacheFilePath(hash);

        if (File.Exists(filePath))
            return await File.ReadAllBytesAsync(filePath);
        return null;
    }

    private string GetCacheFilePath(string hash)
    {
        return Path.Combine(folderPath, $"{hash}.cache");
    }
}