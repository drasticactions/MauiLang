using Drastic.Services;
using Drastic.Tray;
using Drastic.TrayWindow;
using Foundation;
using MauiLang;
using MauiLang.Services;
using MauiLang.ViewModels;
using Microsoft.Maui.Embedding;
using UIKit;

namespace MauiLangEmbedMac;

[Register("AppDelegate")]
public class AppDelegate : TrayAppDelegate
{
    private MauiContext? _mauiContext;
    private UIViewController? controller;
    
    public override UIWindow? Window { get; set; }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MauiLang");
        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path)!);
        var databaseService = new DatabaseService(path);
        var settings = databaseService.GetSettings();
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
        var trayImage = new TrayImage(UIImage.GetSystemImage("photo.circle")!);
        var icon = new Drastic.Tray.TrayIcon(MauiLang.Translations.Common.AppName, trayImage);
        menuItems.Add(new TrayMenuItem(MauiLang.Translations.Common.QuitLabel, null, async () => { Drastic.TrayWindow.NSApplication.Terminate(); }, "q"));
        icon.RightClicked += (object? sender, TrayClickedEventArgs e) => icon.OpenMenu();

        MauiApp mauiApp = builder.Build();
        this._mauiContext = new MauiContext(mauiApp.Services);

        // create a new window instance based on the screen size
        Window = new UIWindow(UIScreen.MainScreen.Bounds);

        // create a UIViewController with a single UILabel
        var vc = new UIViewController();
        vc.View!.AddSubview(new UILabel(Window!.Frame)
        {
            BackgroundColor = UIColor.SystemBackground,
            TextAlignment = UITextAlignment.Center,
            Text = "Hello, Mac Catalyst!",
            AutoresizingMask = UIViewAutoresizing.All,
        });
        Window.RootViewController = vc;

        // make the window visible
        Window.MakeKeyAndVisible();

        return true;
    }
}