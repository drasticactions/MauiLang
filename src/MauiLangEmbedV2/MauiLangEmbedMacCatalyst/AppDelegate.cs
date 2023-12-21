using Drastic.Services;
using Foundation;
using MauiLang;
using MauiLang.Services;
using MauiLang.ViewModels;
using MauiLangEmbed.Controls;
using MauiLangEmbed.Controls.Views;
using Microsoft.Maui.Embedding;
using UIKit;

namespace MauiLangEmbedMacCatalyst;

[Register("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
    MauiContext _mauiContext;

    public override UIWindow? Window { get; set; }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
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
        builder.UseVirtualListView();
        builder.Services.AddSingleton<IErrorHandlerService, MacCatalystErrorHandler>()
            .AddSingleton<IAppDispatcher, MacCatalystAppDispatcher>()
            .AddSingleton<OpenAIService>()
            .AddSingleton<DatabaseService>(databaseService)
            .AddSingleton<Settings>(settings)
            .AddSingleton<SettingsViewModel>()
            .AddSingleton<TargetLanguageViewModel>()
            .AddSingleton<TranslationViewModel>()
            .AddSingleton<SettingsPage>()
            .AddSingleton<LanguageSelectionPage>()
            .AddSingleton<MainPage>()
            .AddSingleton<FavoritesTranslationViewModel>()
            .AddSingleton<FavoritesPage>()
            .AddSingleton<DebugPage>();

        MauiApp mauiApp = builder.Build();
        this._mauiContext = new MauiContext(mauiApp.Services);

        this.Window = new MainUIWindow(_mauiContext, UIScreen.MainScreen.Bounds);
        this.Window.LargeContentTitle = MauiLang.Translations.Common.AppName;

        this.Window.MakeKeyAndVisible();

        return true;
    }
}