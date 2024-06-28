using System.Windows;
using System.Windows.Controls;
using CopperDevs.Core;
using CopperDevs.Rift.Launcher.Data;
using CopperDevs.Rift.Launcher.Utility;

namespace CopperDevs.Rift.Launcher.Views.Pages;

public partial class NewInstancePage : Page
{
    public NewInstancePage()
    {
        InitializeComponent();

        foreach (var version in Enum.GetValues<RiftVersion>())
        {
            VersionComboBox.Items.Add(new ComboBoxItem { Content = version.ToName() });
        }
    }

    private void CreateInstanceButton_OnClick(object sender, RoutedEventArgs e)
    {
        Log.Info(InstanceNameTextBox.Text);

        Log.Info(Enum.GetValues<RiftVersion>()[VersionComboBox.Items.IndexOf(((ComboBoxItem)VersionComboBox.SelectedItem))]);
    }
}