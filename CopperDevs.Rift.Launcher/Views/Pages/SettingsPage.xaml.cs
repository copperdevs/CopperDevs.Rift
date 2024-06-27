using System.Windows.Controls;

namespace CopperDevs.Rift.Launcher.Views.Pages;

public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();

        AppVersionTextBlock.Text = $"App Version - {GetAssemblyVersion()}";
    }


    private static string GetAssemblyVersion()
    {
        return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
    }
}