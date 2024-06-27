using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CopperDevs.Rift.Launcher.Pages;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace CopperDevs.Rift.Launcher;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
    public MainWindow()
    {
        InitializeComponent();
        ApplicationThemeManager.Apply(this);

        var values = Enum.GetValues(typeof(SymbolRegular));
        for (var i = 0; i < 99; i++)
        {
            var randomIcon = (SymbolRegular)values.GetValue(Random.Shared.Next(values.Length))!;
            RootNavigation.MenuItems.Add(new NavigationViewItem(i.ToString(), randomIcon, typeof(HomePage)));
        }

        for (var i = 0; i < 10; i++)
        {
            var randomIcon = (SymbolRegular)values.GetValue(Random.Shared.Next(values.Length))!;

            var navigationViewItem = new NavigationViewItem($"{i} {i}", randomIcon, typeof(HomePage));

            var childObjects = new List<object>();
            
            for (var j = 0; j < 3; j++)
            {
                var randomChildIcon = (SymbolRegular)values.GetValue(Random.Shared.Next(values.Length))!;
                childObjects.Add(new NavigationViewItem($"{i} {j}", randomChildIcon, typeof(HomePage)));
            }

            navigationViewItem.MenuItemsSource = childObjects.ToArray();
            
            RootNavigation.MenuItems.Add(navigationViewItem);
        }
        
    }
}