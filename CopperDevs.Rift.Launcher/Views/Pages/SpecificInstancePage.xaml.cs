using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CopperDevs.Rift.Launcher.Data;
using CopperDevs.Rift.Launcher.Utility;

namespace CopperDevs.Rift.Launcher.Views.Pages;

public partial class SpecificInstancePage : Page
{
    public static SpecificInstancePage Instance = null!;
    
    public SpecificInstancePage()
    {
        Instance = this;
        InitializeComponent();

        foreach (var version in Enum.GetValues<RiftVersion>())
        {
            VersionComboBox.Items.Add(new ComboBoxItem { Content = version.ToName() });
        }
    }

    public void SetData(CreatedInstanceData data)
    {
        InstanceNameTextBlock.Text = $"{data.DisplayName} - {data.RiftVersion}";
    }

    private void PlayButton_OnClick(object sender, RoutedEventArgs args)
    {
        
    }

    private void FolderButton_OnClick(object sender, RoutedEventArgs args)
    {
        
    }

    private void ContentButton_OnClick(object sender, RoutedEventArgs args)
    {
        ModsStackPanel.Visibility = Visibility.Visible;
        SettingsStackPanel.Visibility = Visibility.Collapsed;
    }

    private void OptionsButton_OnClick(object sender, RoutedEventArgs args)
    {
        ModsStackPanel.Visibility = Visibility.Collapsed;
        SettingsStackPanel.Visibility = Visibility.Visible;
    }
}