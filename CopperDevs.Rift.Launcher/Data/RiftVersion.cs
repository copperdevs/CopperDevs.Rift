using CopperDevs.Rift.Launcher.Utility;

namespace CopperDevs.Rift.Launcher.Data;

[Flags]
public enum RiftVersion
{
    [DisplayName("Steam")] // Steam App ID: 2629710
    Steam = 100,

    [DisplayName("Steam (Playtest)")] // Steam App ID: 2880040
    SteamPlaytest = 101,

    // [DisplayName("Steam (Manifest)")]
    // SteamManifest = 102,

    [DisplayName("Beta v0.1.0")]
    V010 = 200,

    [DisplayName("Beta v0.2.0")]
    V020 = 201,

    [DisplayName("Beta v0.3.0")]
    V030 = 202,

    [DisplayName("Beta v0.4.0")]
    V040 = 203,

    [DisplayName("Beta v0.5.1")]
    V051 = 204,
}