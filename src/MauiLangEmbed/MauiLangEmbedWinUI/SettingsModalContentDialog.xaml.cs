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
        private FrameworkElement settingsPage;
        private FrameworkElement outputResponsePage;
        private IModalNavigation modalNavigation;

        public SettingsModalContentDialog(MauiContext context, IModalNavigation modalNavigation)
        {
            this.InitializeComponent();
            this.modalNavigation = modalNavigation;
            this.context = context;
            this.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Microsoft.UI.Xaml.Style;
            var debugPage = context.Services.ResolveWith<SettingsPage>(modalNavigation, this);
            this.outputResponsePage = context.Services.ResolveWith<OutputResponseLanguagePage>(this).ToPlatform(context);
            this.settingsPage = debugPage.ToPlatform(context);
            this.MainContentFrame.Content = this.settingsPage;
        }

        public void ShowLanguageSelectionPage()
        {
            throw new NotImplementedException();
        }

        public void ShowOutputResponseLanguagePage()
        {
            this.MainContentFrame.Content = this.outputResponsePage;
        }

        public void ShowSettingsPage()
        {
            this.MainContentFrame.Content = this.settingsPage;
        }
    }
}
