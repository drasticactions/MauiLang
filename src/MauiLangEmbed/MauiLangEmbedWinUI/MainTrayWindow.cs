using Drastic.Tools;
using Drastic.Tray;
using Drastic.TrayWindow;
using MauiLang;
using Microsoft.Maui.Platform;
using Microsoft.UI.Composition.SystemBackdrops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLangEmbedWinUI;

public class MainTrayWindow : WinUITrayWindow, IModalNavigation
{
    MauiContext context;
    private SettingsModalContentDialog? settingsModal;
    private LanguageSelectionContentDialog? languageSelectionModal;

    public MainTrayWindow(
        MauiContext context,
        TrayIcon icon,
        TrayWindowOptions options)
        : base(icon, options)
    {
        this.context = context;
        var debugPage = context.Services.ResolveWith<MainPage>(this);
        this.Content = debugPage.ToPlatform(context);
        this.SystemBackdrop = new Microsoft.UI.Xaml.Media.DesktopAcrylicBackdrop();
    }

    public void CloseModal()
    {
        this.settingsModal?.Hide();
        this.languageSelectionModal?.Hide();
    }

    public async void OpenLanguageSelectionModal()
    {
        this.languageSelectionModal ??= new LanguageSelectionContentDialog(this.context, this.Content.XamlRoot, this);
        this.languageSelectionModal.XamlRoot = this.Content.XamlRoot;
        await this.languageSelectionModal.ShowAsync();
    }

    public async void OpenSettingsModal()
    {
        this.settingsModal ??= new SettingsModalContentDialog(this.context, this);
        this.settingsModal.XamlRoot = this.Content.XamlRoot;
        await this.settingsModal.ShowAsync();
    }
}
