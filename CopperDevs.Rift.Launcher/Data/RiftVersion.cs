namespace CopperDevs.Rift.Launcher.Data;

[Flags]
public enum RiftVersion
{
    // Latest Steam Build - Steam App ID: 2629710
    Steam = 100,

    // Latest Steam Playtest Build - Steam App ID: 2880040
    SteamPlaytest = 101,

    // Steam Manifest
    SteamManifest = 102,

    // Beta v0.1.0
    V010 = 200,

    // Beta v0.2.0
    V020 = 201,

    // Beta v0.3.0
    V030 = 202,

    // Beta v0.4.0
    V040 = 203,

    // Beta v0.5.1
    V051 = 204,
}