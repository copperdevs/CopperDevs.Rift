using System.IO;
using System.Windows;
using System.Windows.Controls;
using CopperDevs.Core;
using CopperDevs.Rift.Launcher.Data;
using CopperDevs.Rift.Launcher.Views.Pages;
using CopperDevs.Rift.Launcher.Views.Windows;
using Wpf.Ui.Controls;

namespace CopperDevs.Rift.Launcher.Managers;

public static class InstancesData
{
    internal const string InstancesPath = "Instances";

    private static readonly List<CreatedInstanceData> CreatedInstancesData = [];
    private static readonly List<NavigationViewItem> InstancesUiData = [];
    private static readonly List<Page> InstancesPages = [];

    internal static List<CreatedInstanceData> GetAllInstances() => CreatedInstancesData;
    internal static List<NavigationViewItem> GetAllInstancesUi() => InstancesUiData;

    private static NavigationViewItem? currentInstancesItem;

    private static bool hasLoadedBefore = false;

    internal static void LoadInstances(NavigationView navigationView)
    {
        if (hasLoadedBefore || currentInstancesItem is not null)
        {
            var oldInstances = CreatedInstancesData.ToList();

            CreatedInstancesData.Clear();

            LoadPaths();

            var newInstances = new List<CreatedInstanceData>();

            foreach (var instance in CreatedInstancesData)
            {
                var shouldContinue = false;
                foreach (var oldInstance in oldInstances.Where(oldInstance => oldInstance.FileName == instance.FileName)) shouldContinue = true;
                if (shouldContinue) continue;

                Log.Debug($"old instance doesnt contains instance named {instance.DisplayName}");
                newInstances.Add(instance);
            }

            for (var i = 0; i < newInstances.Count; i++)
            {
                var instance = newInstances[i];
                var createdItem = new NavigationViewItem(instance.DisplayName, SymbolRegular.Games24, typeof(SpecificInstancePage))
                {
                    IsManipulationEnabled = false,
                    Tag = i
                };
                InstancesUiData.Add(createdItem);
                currentInstancesItem?.MenuItems.Add(createdItem);
            }

            CreatedInstancesData.Sort((data1, data2) => string.Compare(data1.FileName, data2.FileName, StringComparison.Ordinal));

            for (var i = 0; i < currentInstancesItem?.MenuItems.Count; i++)
            {
                var item = (NavigationViewItem)currentInstancesItem.MenuItems[i]!;

                var data = CreatedInstancesData[i];

                item.Content = data.DisplayName;
                item.Tag = i;
            }
        }
        else
        {
            hasLoadedBefore = true;

            LoadPaths();
            CreatedInstancesData.Sort((data1, data2) => string.Compare(data1.FileName, data2.FileName, StringComparison.Ordinal));
            
            for (var i = 0; i < CreatedInstancesData.Count; i++)
            {
                var instance = CreatedInstancesData[i];
                var createdItem = new NavigationViewItem(instance.DisplayName, SymbolRegular.Games24, typeof(SpecificInstancePage))
                {
                    IsManipulationEnabled = false,
                    Tag = i
                };
                InstancesUiData.Add(createdItem);
            }

            currentInstancesItem = new NavigationViewItem("Instances", SymbolRegular.Folder24, typeof(AllInstancesPage), InstancesUiData);
            navigationView.MenuItems.Add(currentInstancesItem);
        }
    }

    private static void LoadPaths()
    {
        if (!Directory.Exists(InstancesPath))
            return;

        foreach (var instancePath in Directory.EnumerateFiles(InstancesPath))
        {
            var loadedInstance = CreatedInstanceData.LoadFromFile(instancePath);
            if (loadedInstance is not null)
                CreatedInstancesData.Add(loadedInstance);
        }
    }

    internal static void NavigationViewItemClicked(NavigationViewItem item)
    {
        if (!InstancesUiData.Contains(item))
            return;

        var index = InstancesUiData.IndexOf(item);

        SpecificInstancePage.Instance.SetData(CreatedInstancesData[index]);
    }

    internal static void CreateInstance(string name, RiftVersion version)
    {
        if (string.IsNullOrWhiteSpace(name))
            return;

        var createdInstanceData = new CreatedInstanceData { DisplayName = name, RiftVersion = version };
        createdInstanceData.SaveToFile();

        LoadInstances(((MainWindow)Application.Current.MainWindow!).RootNavigation);
    }
}