using Drastic.Tools;
using MauiLang;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MauiLangEmbedWinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Microsoft.UI.Xaml.Window, INativeNavigation
    {
        MauiContext context;

        public MainWindow(MauiContext context)
        {
            this.InitializeComponent();
            this.context = context;
            var debugPage = context.Services.ResolveWith<MainPage>(this);
            this.Content = debugPage.ToPlatform(context);
            this.SystemBackdrop = new Microsoft.UI.Xaml.Media.DesktopAcrylicBackdrop();
        }

        public void CloseModal()
        {
            throw new NotImplementedException();
        }

        public async void OpenModal()
        {
            var contentDialog = new SettingsModalContentDialog(this.context);
            contentDialog.XamlRoot = this.Content.XamlRoot;
            await contentDialog.ShowAsync();
        }

        public void SetPage(object page)
        {
            throw new NotImplementedException();
        }

        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var contentDialog = new SettingsModalContentDialog(this.context);
        //    contentDialog.XamlRoot = this.Content.XamlRoot;
        //    // var debugPage = context.Services.GetRequiredService<DebugPage>();
        //    // contentDialog.Content = debugPage.ToPlatform(context);
        //    await contentDialog.ShowAsync();
        //}
    }
}
