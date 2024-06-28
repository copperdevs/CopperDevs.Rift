using System.IO;
using System.IO.Compression;
using CopperDevs.Core;
using CopperDevs.Rift.Launcher.Utility;

namespace CopperDevs.Rift.Launcher.Managers;

public static class ModLoaderManager
{
    private static string bepInExDownloadUrl = "";
    private const string BuildPath = "Builds/Temporary/BepInEx_win_x64_5.4.23.2.zip";

    private static bool CurrentlyDownloaded => File.Exists(BuildPath);

    internal static void SetDownloadUrl()
    {
        bepInExDownloadUrl = ResourceLoader.LoadTextResource("CopperDevs.Rift.Launcher.Data.BepInExLink.txt");
    }

    internal static async void CopyToDirectory(string directory)
    {
        if (CurrentlyDownloaded)
        {
            ZipFile.ExtractToDirectory(BuildPath, directory, false);
        }
        else
        {
            await Util.DownloadFileWithProgressAsync(bepInExDownloadUrl, BuildPath,
                null!,
                () =>
                {
                    Log.Success($"Finished downloading build {BuildPath}");
                });
            ZipFile.ExtractToDirectory(BuildPath, directory, false);
        }
    }
}