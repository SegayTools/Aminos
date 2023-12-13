using System.Threading.Tasks;

namespace AminosUI.Services.Caches;

public interface ICacheManager
{
    public ValueTask SaveCache(string hash, byte[] data);
    public ValueTask<byte[]> LoadCache(string hash);
}