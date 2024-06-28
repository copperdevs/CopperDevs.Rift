using System.Diagnostics;
using System.IO;
using System.Net.Http;
using CopperDevs.Core;
using CopperDevs.Core.Utility;
using Microsoft.Win32;

namespace CopperDevs.Rift.Launcher.Utility;

public static class Util
{
    public static T GetAttribute<T>(this Enum value) where T : Attribute
    {
        var type = value.GetType();
        var memberInfo = type.GetMember(value.ToString());
        var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
        return (attributes.Length > 0 ? (T)attributes[0] : null)!;
    }

    public static string ToName(this Enum value)
    {
        var attribute = value.GetAttribute<DisplayNameAttribute>();
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return attribute is null ? value.ToString() : attribute.Description;
    }

    public static void RunSteamGame(ulong appId)
    {
        if (WindowsApi.IsWindows)
            Process.Start((Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamExe", null) as string)!, $"steam://rungameid/{appId}");
        else
            Log.Error("this is a windows app bruh fuck linux&macos idc about them | actual error: Attempting to run a steam game on a non windows platform, when currently only windows is supported");
    }
    
    public static async Task DownloadFileWithProgressAsync(string url, string destinationPath, Action<int> progressUpdate = null!, Action finishedDownloading = null!)
    {
        using var client = new HttpClient();
        using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        
        response.EnsureSuccessStatusCode();

        var totalBytes = response.Content.Headers.ContentLength;

        await using Stream contentStream = await response.Content.ReadAsStreamAsync(), fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
        
        var buffer = new byte[8192];
        long totalBytesRead = 0;
        var bytesRead = 0;
        var progressPercentage = 0;
        var lastProgressPercentage = -1;

        while ((bytesRead = await contentStream.ReadAsync(buffer)) != 0)
        {
            await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
            totalBytesRead += bytesRead;

            if (totalBytes.HasValue)
            {
                progressPercentage = (int)((totalBytesRead * 100) / totalBytes.Value);
                
                if (progressPercentage == lastProgressPercentage) 
                    continue;
                
                Log.Network($"Download progress: {progressPercentage}%");
                
                progressUpdate?.Invoke(progressPercentage);
                lastProgressPercentage = progressPercentage;
            }
            else
            {
                Log.Network($"Downloaded {totalBytesRead} bytes");
            }
        }
        
        finishedDownloading?.Invoke();
    }
}