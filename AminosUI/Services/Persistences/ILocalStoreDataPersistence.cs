using System.Threading.Tasks;

namespace AminosUI.Services.Persistences;

public interface ILocalStoreDataPersistence
{
    ValueTask Save<T>(string key, T obj);
    ValueTask<T> Load<T>(string key) where T : new();
}