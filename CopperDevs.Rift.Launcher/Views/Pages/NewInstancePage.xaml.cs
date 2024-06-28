using System.Windows;
using System.Windows.Controls;
using CopperDevs.Core;
using CopperDevs.Rift.Launcher.Data;
using CopperDevs.Rift.Launcher.Managers;
using CopperDevs.Rift.Launcher.Utility;

namespace CopperDevs.Rift.Launcher.Views.Pages;

public partial class NewInstancePage : Page
{
    private readonly object defaultVersionValue;
    public NewInstancePage()
    {
        InitializeComponent();

        foreach (var version in Enum.GetValues<RiftVersion>())
        {
            VersionComboBox.Items.Add(new ComboBoxItem { Content = version.ToName() });
        }

        defaultVersionValue = VersionComboBox.Items[0]!;
    }

    private void CreateInstanceButton_OnClick(object sender, RoutedEventArgs e)
    {
        GameInstancesData.CreateInstance(InstanceNameTextBox.Text, Enum.GetValues<RiftVersion>()[VersionComboBox.Items.IndexOf(((ComboBoxItem)VersionComboBox.SelectedItem))]);

        InstanceNameTextBox.Text = "";
        VersionComboBox.SelectedItem = defaultVersionValue;
    }
}