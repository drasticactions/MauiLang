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
    public sealed partial class SettingsModalContentDialog : ContentDialog, INativeNavigation
    {
        private MauiContext context;

        public SettingsModalContentDialog(MauiContext context)
        {
            this.context = context;
            this.InitializeComponent();
            this.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Microsoft.UI.Xaml.Style;
            var debugPage = context.Services.ResolveWith<SettingsPage>(this);
            this.MainContentFrame.Content = debugPage.ToPlatform(context);
        }

        public void CloseModal()
        {
            this.Hide();
        }

        public void OpenModal()
        {
            throw new NotImplementedException();
        }

        public void SetPage(object page)
        {
            this.MainContentFrame.Content = page;
        }
    }
}
