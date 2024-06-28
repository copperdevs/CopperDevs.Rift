using System.IO;
using System.Text.Json;
using CopperDevs.Rift.Launcher.Managers;

namespace CopperDevs.Rift.Launcher.Data;

public class CreatedInstanceData
{
    public string DisplayName { get; set; } = "";
    public RiftVersion RiftVersion { get; set; } = RiftVersion.Steam;
    public string FileName { get; set; } = "";

    public static CreatedInstanceData? LoadFromFile(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<CreatedInstanceData>(json);
    }

    public void SaveToFile(string path = GameInstancesData.InstancesPath)
    {
        Directory.CreateDirectory(GameInstancesData.InstancesPath);

        if (string.IsNullOrEmpty(FileName))
            FileName = Guid.NewGuid().ToString();

        var json = JsonSerializer.Serialize(this);
        File.WriteAllText($"{path}/{FileName}.json", json);
    }
}