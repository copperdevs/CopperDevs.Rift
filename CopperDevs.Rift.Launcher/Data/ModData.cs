using CommunityToolkit.Mvvm.ComponentModel;

namespace CopperDevs.Rift.Launcher.Data;

public class ModData
{
    public string ModName { get; set; } = "Unnamed";
    public RiftVersion TargetVersion { get; set; }
}