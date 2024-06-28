using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CopperDevs.Rift.Launcher.Data;

namespace CopperDevs.Rift.Launcher.Views.Pages;

public partial class SpecificInstancePage : Page
{
    public static SpecificInstancePage Instance = null!;
    
    public SpecificInstancePage()
    {
        Instance = this;
        InitializeComponent();
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
    }

    private void OptionsButton_OnClick(object sender, RoutedEventArgs args)
    {
        ModsStackPanel.Visibility = Visibility.Collapsed;
    }
}