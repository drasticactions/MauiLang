﻿// <copyright file="MauiProgram.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;
using MauiLang.Services;
using MauiLang.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiLang;

/// <summary>
/// Maui Program.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Main.
    /// </summary>
    /// <returns>MauiApp.</returns>
    public static MauiApp CreateMauiApp()
    {
#if MACCATALYST
        // Changes buttons to match iPad behavior.
        // This allows us to keep colors and styles.
        Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping("ButtonChange", (handler, view) =>
        {
            handler.PlatformView.PreferredBehavioralStyle = UIKit.UIBehavioralStyle.Pad;
            handler.PlatformView.Layer.CornerRadius = 5;
            handler.PlatformView.ClipsToBounds = true;
        });

        // Adds toolbar to window.
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping("WindowChange", (handler, view) =>
        {
            if (handler.PlatformView?.WindowScene?.Titlebar != null && view is Window win)
            {
                var toolbar = new AppKit.NSToolbar();
                toolbar.Delegate = new ToolbarDelegate(win);
                toolbar.DisplayMode = AppKit.NSToolbarDisplayMode.Icon;

                handler.PlatformView.WindowScene.Titlebar.Toolbar = toolbar;
                handler.PlatformView.WindowScene.Titlebar.ToolbarStyle = UIKit.UITitlebarToolbarStyle.Automatic;
                handler.PlatformView.WindowScene.Titlebar.Toolbar.Visible = true;
            }
        });
#endif

#if WINDOWS
        // Adds Mica backdrop, removes colors for title bar buttons.
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping("WindowChange", (handler, view) =>
        {
            handler.PlatformView.SystemBackdrop = new Microsoft.UI.Xaml.Media.MicaBackdrop();
            handler.PlatformView.Title = "MauiLang";
            handler.PlatformView.AppWindow.Title = "MauiLang";
            handler.PlatformView.AppWindow.TitleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;
            handler.PlatformView.AppWindow.TitleBar.BackgroundColor = Microsoft.UI.Colors.Transparent;
            handler.PlatformView.AppWindow.TitleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
            handler.PlatformView.AppWindow.TitleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;
            handler.PlatformView.AppWindow.TitleBar.ButtonForegroundColor = Microsoft.UI.Colors.Transparent;
            handler.PlatformView.AppWindow.TitleBar.ButtonInactiveForegroundColor = Microsoft.UI.Colors.Transparent;
        });
#endif

#if IOS || MACCATALYST
        // Forces placeholder text to appeared center aligned.
        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("EntryChange", (handler, view) =>
        {
            var centeredParagraphStyle = new UIKit.NSMutableParagraphStyle();
            centeredParagraphStyle.Alignment = UIKit.UITextAlignment.Left;
            var attributedPlaceholder = new Foundation.NSAttributedString(
                view.Placeholder,
                new UIKit.UIStringAttributes
                {
                    ParagraphStyle = centeredParagraphStyle,
                });

            handler.PlatformView.AttributedPlaceholderText = attributedPlaceholder;
        });
#endif

        var databaseService = new DatabaseService();
        var settings = databaseService.GetSettings();
        var builder = MauiApp.CreateBuilder();
        builder.Services
            .AddSingleton<IAppDispatcher, MauiAppDispatcher>()
            .AddSingleton<IErrorHandlerService, MauiErrorHandler>()
            .AddSingleton<OpenAIService>()
            .AddSingleton<DatabaseService>(databaseService)
            .AddSingleton<Settings>(settings)
            .AddSingleton<SettingsViewModel>()
            .AddSingleton<SettingsPage>()
            .AddSingleton<LanguageSelectionPage>()
            .AddSingleton<TranslationViewModel>()
            .AddSingleton<ModalNavigationSettingsPage>()
            .AddSingleton<OutputResponseLanguageViewModel>()
            .AddSingleton<OutputResponseLanguagePage>()
            .AddSingleton<ModalTranslationSettingsPage>()
            .AddSingleton<TargetLanguageViewModel>();
        builder
            .UseMauiApp<App>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}