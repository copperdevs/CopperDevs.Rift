using System.Windows;
using System.Windows.Controls;
using CopperDevs.Core;
using CopperDevs.Rift.Launcher.Managers;
using CopperDevs.Rift.Launcher.Views.Windows;
using Button = Wpf.Ui.Controls.Button;

namespace CopperDevs.Rift.Launcher.Views.Pages;

public partial class AllInstancesPage : Page
{
    public AllInstancesPage()
    {
        InitializeComponent();

        foreach (var instance in InstancesData.GetAllInstances())
        {
            var button = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Content = instance.DisplayName,
                Height = 65
            };

            // TODO: Make this work better bruh
            button.Click += (sender, args) =>
            {
                var instances = InstancesData.GetAllInstances();
                var uiInstances = InstancesData.GetAllInstancesUi();
                var rootNavigation = ((MainWindow)Application.Current.MainWindow!).RootNavigation;

                var tag = uiInstances[instances.IndexOf(instance)].Tag.ToString();

                Log.Debug($"tag: {tag} | instance null: {instance is null} | navigation null: {rootNavigation is null} | instance page null: {SpecificInstancePage.Instance is null}");

                try
                {
                    var newPage = new SpecificInstancePage();

                    NavigationService.Navigate(newPage);
                    newPage.SetData(instance);

                    rootNavigation?.Navigate(tag);
                    SpecificInstancePage.Instance?.SetData(instance);
                }
                catch (Exception e)
                {
                    Log.Exception(e);
                }
            };

            var seperator = new Separator
            {
                Opacity = 0,
                Height = 5
            };

            InstancesStackPanel.Children.Add(button);
            InstancesStackPanel.Children.Add(seperator);
        }
    }
}