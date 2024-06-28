﻿using System.Windows;
using CopperDevs.Core;
using CopperDevs.Rift.Launcher.Managers;
using CopperDevs.Rift.Launcher.Views.Pages;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace CopperDevs.Rift.Launcher.Views.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
    public MainWindow()
    {
        CopperLogger.IncludeTimestamps = true;

        InitializeComponent();
        ApplicationThemeManager.Apply(this);

        GameInstancesData.LoadInstances(RootNavigation);
        GameDownloadManager.SetDownloadUrls();
    }

    private void RootNavigation_OnSelectionChanged(NavigationView sender, RoutedEventArgs args)
    {
        GameInstancesData.NavigationViewItemClicked((NavigationViewItem)sender.SelectedItem!);
    }
}