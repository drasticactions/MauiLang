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

namespace MauiLangEmbedWinUI
{
    public sealed partial class LanguageSelectionContentDialog : ContentDialog
    {
        private MauiContext context;
        private FrameworkElement settingsPage;

        public LanguageSelectionContentDialog(MauiContext context, XamlRoot xamlRoot, IModalNavigation modalNavigation)
        {
            this.InitializeComponent();
            this.context = context;
            this.XamlRoot = xamlRoot;
            this.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Microsoft.UI.Xaml.Style;
            var debugPage = context.Services.ResolveWith<LanguageSelectionPage>(modalNavigation);
            this.settingsPage = debugPage.ToPlatform(context);
            this.MainContentFrame.Content = this.settingsPage;
        }
    }
}
