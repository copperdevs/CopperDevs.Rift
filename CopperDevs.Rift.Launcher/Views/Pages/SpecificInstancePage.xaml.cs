using System.Windows;
using System.Windows.Controls;
using CopperDevs.Rift.Launcher.Data;
using CopperDevs.Rift.Launcher.Utility;

namespace CopperDevs.Rift.Launcher.Views.Pages;

public partial class SpecificInstancePage : Page
{
    public static SpecificInstancePage Instance = null!;

    private CreatedInstanceData currentData = null!;

    public SpecificInstancePage()
    {
        Instance = this;
        InitializeComponent();
        // return;
        foreach (var version in Enum.GetValues<RiftVersion>())
        {
            VersionComboBox.Items.Add(new ComboBoxItem { Content = version.ToName() });
        }
    }

    public void SetData(CreatedInstanceData data)
    {
        currentData = data;
        InstanceNameTextBlock.Text = $"{data.DisplayName}";
        VersionComboBox.SelectedItem = VersionComboBox.Items[Enum.GetValues<RiftVersion>().ToList().IndexOf(data.RiftVersion)];
    }

    private void PlayButton_OnClick(object sender, RoutedEventArgs args)
    {
        GameDownloadManager.RunBuild(Enum.GetValues<RiftVersion>()[VersionComboBox.Items.IndexOf(VersionComboBox.SelectedItem)]);
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

    private void VersionComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (currentData is null)
            return;

        currentData.RiftVersion = Enum.GetValues<RiftVersion>()[VersionComboBox.Items.IndexOf(e.AddedItems[0]!)];
        currentData.SaveToFile();
    }
}