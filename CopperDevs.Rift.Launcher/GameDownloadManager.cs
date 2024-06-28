using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using CopperDevs.Core;
using CopperDevs.Rift.Launcher.Data;
using CopperDevs.Rift.Launcher.Utility;

namespace CopperDevs.Rift.Launcher;

public static class GameDownloadManager
{
    private const ulong MainAppId = 2629710;
    private const ulong PlaytestAppId = 2880040;

    private const string GameBuildsDirectory = "Builds";

    private const string BuildV010Path = "V010";
    private static bool IsBuildV010Downloaded => File.Exists($"{GameBuildsDirectory}/{BuildV010Path}/Rift.exe");
    private static string buildV010DownloadUrl = null!;

    private const string BuildV020Path = "V020";
    private static bool IsBuildV020Downloaded => File.Exists($"{GameBuildsDirectory}/{BuildV020Path}/Rift.exe");
    private static string buildV020DownloadUrl = null!;

    private const string BuildV030Path = "V030";
    private static bool IsBuildV030Downloaded => File.Exists($"{GameBuildsDirectory}/{BuildV030Path}/Rift.exe");
    private static string buildV030DownloadUrl = null!;

    private const string BuildV040Path = "V040";
    private static bool IsBuildV040Downloaded => File.Exists($"{GameBuildsDirectory}/{BuildV040Path}/RIFT.exe");
    private static string buildV040DownloadUrl = null!;

    private const string BuildV051Path = "V051";
    private static bool IsBuildV051Downloaded => File.Exists($"{GameBuildsDirectory}/{BuildV051Path}/RIFT.exe");
    private static string buildV051DownloadUrl = null!;

    public static async void RunBuild(RiftVersion version)
    {
        switch (version)
        {
            case RiftVersion.Steam:
                Util.RunSteamGame(MainAppId);
                break;
            case RiftVersion.SteamPlaytest:
                Util.RunSteamGame(PlaytestAppId);
                break;
            case RiftVersion.V010:
                if (IsBuildV010Downloaded)
                    Process.Start($"{GameBuildsDirectory}/{BuildV010Path}/RIFT.exe");
                else
                    await DownloadBuild(version);
                break;
            case RiftVersion.V020:
                if (IsBuildV020Downloaded)
                    Process.Start($"{GameBuildsDirectory}/{BuildV020Path}/RIFT.exe");
                else
                    await DownloadBuild(version);
                break;
            case RiftVersion.V030:
                if (IsBuildV030Downloaded)
                    Process.Start($"{GameBuildsDirectory}/{BuildV030Path}/RIFT.exe");
                else
                    await DownloadBuild(version);
                break;
            case RiftVersion.V040:
                if (IsBuildV040Downloaded)
                    Process.Start($"{GameBuildsDirectory}/{BuildV040Path}/RIFT.exe");
                else
                    await DownloadBuild(version);
                break;
            case RiftVersion.V051:
                if (IsBuildV051Downloaded)
                    Process.Start($"{GameBuildsDirectory}/{BuildV051Path}/RIFT.exe");
                else
                    await DownloadBuild(version);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(version), version, null);
        }
    }

    private static async Task DownloadBuild(RiftVersion version)
    {
        var url = version switch
        {
            RiftVersion.V010 => buildV010DownloadUrl,
            RiftVersion.V020 => buildV020DownloadUrl,
            RiftVersion.V030 => buildV030DownloadUrl,
            RiftVersion.V040 => buildV040DownloadUrl,
            RiftVersion.V051 => buildV051DownloadUrl,
            _ => ""
        };

        var path = version switch
        {
            RiftVersion.V010 => BuildV010Path,
            RiftVersion.V020 => BuildV020Path,
            RiftVersion.V030 => BuildV030Path,
            RiftVersion.V040 => BuildV040Path,
            RiftVersion.V051 => BuildV051Path,
            _ => ""
        };

        if (string.IsNullOrEmpty(url))
            return;

        if (!Directory.Exists(GameBuildsDirectory))
            Directory.CreateDirectory(GameBuildsDirectory);

        if (!Directory.Exists($"{GameBuildsDirectory}/Temporary/"))
            Directory.CreateDirectory($"{GameBuildsDirectory}/Temporary/");

        await Util.DownloadFileWithProgressAsync(url, $"{GameBuildsDirectory}/Temporary/temp.zip",
            progress => Log.Network($"Download progress: {progress}%"),
            () => Log.Success($"Finished downloading build {path}"));

        ZipFile.ExtractToDirectory($"{GameBuildsDirectory}/Temporary/temp.zip", $"{GameBuildsDirectory}/{path}");
    }

    public static void SetDownloadUrls()
    {
        var rawText = ResourceLoader.LoadTextResource("CopperDevs.Rift.Launcher.RiftBuildLinks.txt");
        var lines = rawText.Split("|");

        buildV010DownloadUrl = lines[0];
        buildV020DownloadUrl = lines[1];
        buildV030DownloadUrl = lines[2];
        buildV040DownloadUrl = lines[3];
        buildV051DownloadUrl = lines[4];
    }
}