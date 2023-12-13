using System.Threading;
using System.Threading.Tasks;

namespace AminosUI.Services.Applications;

public interface IImageLoader
{
    Task<byte[]> LoadImage(string url, CancellationToken cancellationToken);
}