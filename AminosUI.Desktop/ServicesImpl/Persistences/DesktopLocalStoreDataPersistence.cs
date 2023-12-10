using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Aminos.Core.Services.Injections.Attrbutes;
using AminosUI.Services.Persistences;

namespace AminosUI.Desktop.ServicesImpl.Persistences;

[RegisterInjectable(typeof(ILocalStoreDataPersistence))]
public class DesktopLocalStoreDataPersistence : ILocalStoreDataPersistence
{
    private readonly string savePath;
    private Dictionary<string, string> settingMap;

    public DesktopLocalStoreDataPersistence()
    {
        savePath = Path.Combine(Path.GetDirectoryName(typeof(DesktopLocalStoreDataPersistence).Assembly.Location),
            "setting.json");
    }

    public async ValueTask Save<T>(string key, T obj)
    {
        settingMap[key] = JsonSerializer.Serialize(obj);
        var content = JsonSerializer.Serialize(settingMap);
        await File.WriteAllTextAsync(savePath, content);
    }

    public async ValueTask<T> Load<T>(string key) where T : new()
    {
        if (settingMap is null)
            if (File.Exists(savePath))
            {
                var content = await File.ReadAllTextAsync(savePath);
                settingMap = JsonSerializer.Deserialize<Dictionary<string, string>>(content);
            }
            else
            {
                settingMap = new Dictionary<string, string>();
            }

        return settingMap.TryGetValue(key, out var jsonContent) ? JsonSerializer.Deserialize<T>(jsonContent) : new T();
    }
}