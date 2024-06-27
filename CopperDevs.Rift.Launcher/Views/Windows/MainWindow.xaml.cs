using System.Windows;
using CopperDevs.Core;
using CopperDevs.Rift.Launcher.Views.Pages;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace CopperDevs.Rift.Launcher.Views.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
    private readonly List<object> childObjects;

    public MainWindow()
    {
        CopperLogger.IncludeTimestamps = true;

        InitializeComponent();
        ApplicationThemeManager.Apply(this);
        
        childObjects = new List<object>();
        
        for (var i = 0; i < 10; i++)
        {
            var createdItem = new NavigationViewItem($"Instance {i}", SymbolRegular.Games24, typeof(SpecificInstancePage))
            {
                NavigationCacheMode = NavigationCacheMode.Enabled,
                Tag = $"Instance {i}"
            };
            childObjects.Add(createdItem);
        }

        RootNavigation.MenuItems.Add(new NavigationViewItem("Instances", SymbolRegular.Folder24, typeof(AllInstancesPage), childObjects));
    }

    private void RootNavigation_OnSelectionChanged(NavigationView sender, RoutedEventArgs args)
    {
        Log.Info($"selection {sender.SelectedItem} | index {childObjects.IndexOf(sender.SelectedItem!)}");
    }
}