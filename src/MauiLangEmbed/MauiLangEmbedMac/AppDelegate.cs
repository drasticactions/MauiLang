using Drastic.Services;
using Drastic.Tools;
using Drastic.Tray;
using Drastic.TrayWindow;
using Foundation;
using MauiLang;
using MauiLang.Services;
using MauiLang.ViewModels;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Platform;
using UIKit;

namespace MauiLangEmbedMac;

[Register("AppDelegate")]
public class AppDelegate : TrayAppDelegate, IModalNavigation, INativeNavigation
{
    private MauiContext? _mauiContext;
    private UIViewController? controller;
    private UIViewController? settingsViewController;
    private UIViewController? languageSelectionViewController;
    private UINavigationController settingsNavigationController;
    private SettingsPage? settingsPage;
    private LanguageSelectionPage? languageSelectionPage;
    private MainPage? page;
    
    public override UIWindow? Window { get; set; }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        NSApplication.SetActivationPolicy(NSApplicationActivationPolicy.Accessory);
        var databaseService = new DatabaseService();
        var settings = databaseService.GetSettings();

        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("EntryChange", (handler, view) =>
        {
            var centeredParagraphStyle = new UIKit.NSMutableParagraphStyle();
            centeredParagraphStyle.Alignment = UIKit.UITextAlignment.Center;
            var attributedPlaceholder = new Foundation.NSAttributedString(
                view.Placeholder,
                new UIKit.UIStringAttributes
                {
                    ParagraphStyle = centeredParagraphStyle,
                });

            handler.PlatformView.AttributedPlaceholderText = attributedPlaceholder;
        });

        Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping("ButtonChange", (handler, view) =>
        {
            handler.PlatformView.PreferredBehavioralStyle = UIKit.UIBehavioralStyle.Pad;
            handler.PlatformView.Layer.CornerRadius = 5;
            handler.PlatformView.ClipsToBounds = true;
        });

        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();

        builder.Services.AddSingleton<IErrorHandlerService, MacCatalystErrorHandler>()
            .AddSingleton<IAppDispatcher, MacCatalystAppDispatcher>()
            .AddSingleton<OpenAIService>()
            .AddSingleton<DatabaseService>(databaseService)
            .AddSingleton<Settings>(settings)
            .AddSingleton<OutputResponseLanguageViewModel>()
            .AddSingleton<SettingsViewModel>()
            .AddSingleton<TargetLanguageViewModel>()
            .AddSingleton<TranslationViewModel>()
            .AddSingleton<SettingsPage>()
            .AddSingleton<OutputResponseLanguagePage>()
            .AddSingleton<LanguageSelectionPage>()
            .AddSingleton<MainPage>()
            .AddSingleton<DebugPage>();

        var menuItems = new List<TrayMenuItem>();

        // Temp logo.
        var trayImage = new TrayImage(UIImage.GetSystemImage("bubble.left.and.text.bubble.right.fill")!);
        menuItems.Add(new TrayMenuItem(MauiLang.Translations.Common.QuitLabel, null, async () => { Drastic.TrayWindow.NSApplication.Terminate(); }, "q"));
        var icon = new Drastic.Tray.TrayIcon(MauiLang.Translations.Common.AppName, trayImage, menuItems);
        icon.RightClicked += (object? sender, TrayClickedEventArgs e) => icon.OpenMenu();

        MauiApp mauiApp = builder.Build();
        this._mauiContext = new MauiContext(mauiApp.Services);

        this.page = this._mauiContext.Services.ResolveWith<MainPage>(this);
        this.settingsPage = this._mauiContext.Services.ResolveWith<SettingsPage>(this, this);
        this.languageSelectionPage = this._mauiContext.Services.ResolveWith<LanguageSelectionPage>(this);
        this.controller = this.page.ToUIViewController(this._mauiContext);
        this.settingsViewController = this.settingsPage.ToUIViewController(this._mauiContext);
        this.settingsViewController.PreferredContentSize = new CoreGraphics.CGSize(250, 300);
        this.settingsViewController.ModalPresentationStyle = UIModalPresentationStyle.Automatic;
        this.languageSelectionViewController = this.languageSelectionPage.ToUIViewController(this._mauiContext);
        this.languageSelectionViewController.PreferredContentSize = new CoreGraphics.CGSize(250, 300);
        this.languageSelectionViewController.ModalPresentationStyle = UIModalPresentationStyle.Automatic;
        this.CreateTrayWindow(icon, new TrayWindowOptions(), this.controller);

        return true;
    }

    public async void OpenSettingsModal()
    {
        await this.controller?.PresentViewControllerAsync(this.settingsViewController, true);
    }

    public async void OpenLanguageSelectionModal()
    {
        await this.controller?.PresentViewControllerAsync(this.languageSelectionViewController, true);
    }

    public void CloseModal()
    {
        this.controller?.DismissViewControllerAsync(true);
    }

    public async void ShowSettingsPage()
    {
        throw new NotImplementedException();
    }

    public void ShowOutputResponseLanguagePage()
    {
        throw new NotImplementedException();
    }

    public void ShowLanguageSelectionPage()
    {
        throw new NotImplementedException();
    }
}