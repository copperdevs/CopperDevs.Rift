using System.Windows.Controls;
using CopperDevs.Rift.Launcher.Data;
using CopperDevs.Rift.Launcher.Views.Pages;
using Wpf.Ui.Controls;

namespace CopperDevs.Rift.Launcher;

public static class GameInstancesData
{
    private static readonly List<CreatedInstanceData> CreatedInstancesData = [];
    private static readonly List<NavigationViewItem> InstancesUiData = [];
    private static readonly List<Page> InstancesPages = [];

    internal static List<CreatedInstanceData> GetAllInstances() => CreatedInstancesData;
    internal static List<NavigationViewItem> GetAllInstancesUi() => InstancesUiData;

    internal static void LoadInstancesData()
    {
        CreatedInstancesData.Clear();

        var values = Enum.GetValues(typeof(RiftVersion));

        for (var i = 0; i < 10; i++)
        {
            CreatedInstancesData.Add(new CreatedInstanceData
            {
                DisplayName = $"Test Instance {i}",
                RiftVersion = (RiftVersion)values.GetValue(Random.Shared.Next(values.Length))!,
            });
        }
    }

    internal static void LoadInstancesUi(ref NavigationView navigationView)
    {
        InstancesUiData.Clear();

        for (var i = 0; i < CreatedInstancesData.ToList().Count; i++)
        {
            var instance = CreatedInstancesData.ToList()[i];
            var createdItem = new NavigationViewItem(instance.DisplayName, SymbolRegular.Games24, typeof(SpecificInstancePage))
            {
                NavigationCacheMode = NavigationCacheMode.Enabled,
                IsManipulationEnabled = false,
                Tag = i
            };
            InstancesUiData.Add(createdItem);
        }

        navigationView.MenuItems.Add(new NavigationViewItem("Instances", SymbolRegular.Folder24, typeof(AllInstancesPage), InstancesUiData));
    }

    internal static void NavigationViewItemClicked(NavigationViewItem item)
    {
        if (!InstancesUiData.Contains(item))
            return;

        var index = InstancesUiData.IndexOf(item);

        SpecificInstancePage.Instance.SetData(CreatedInstancesData[index]);
    }
}