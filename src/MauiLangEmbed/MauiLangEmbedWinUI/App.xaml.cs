﻿using Drastic.Services;
using MauiLang;
using MauiLang.Services;
using MauiLang.ViewModels;
using MauiLangEmbedWinUI.Tools;
using Microsoft.Maui.Embedding;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MauiLangEmbedWinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Microsoft.UI.Xaml.Application
    {
        private readonly SingleInstanceDesktopApp _singleInstanceApp;

        public MauiContext? _mauiContext;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            _singleInstanceApp = new SingleInstanceDesktopApp("33a911d3-d13d-4768-85be-b61a7e0eedbc");
            _singleInstanceApp.Launched += OnSingleInstanceLaunched;
        }

        private void OnSingleInstanceLaunched(object? sender, SingleInstanceLaunchEventArgs e)
        {
            if (e.IsFirstLaunch)
            {
                string databaseField = WinUIExtensions.IsRunningAsUwp() ? Windows.Storage.ApplicationData.Current.LocalFolder.Path : (System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly()!.Location!)!);
                var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
                MauiAppBuilder builder = MauiApp.CreateBuilder();
                var databaseService = new DatabaseService(databaseField);
                var settings = databaseService.GetSettings();
                builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();
                builder.Services
                    .AddSingleton<IErrorHandlerService>(new WinUIErrorHandler())
                    .AddSingleton<IAppDispatcher>(new AppDispatcher(dispatcherQueue))
                    .AddSingleton<OpenAIService>()
                    .AddSingleton<DatabaseService>(databaseService)
                    .AddSingleton<Settings>(settings)
                    .AddSingleton<OutputResponseLanguageViewModel>()
                    .AddSingleton<SettingsViewModel>()
                    .AddSingleton<TargetLanguageViewModel>()
                    .AddSingleton<TranslationViewModel>()
                    .AddSingleton<SettingsPage>()
                    .AddSingleton<OutputResponseLanguagePage>()
                    .AddSingleton<MainPage>()
                    .AddSingleton<DebugPage>();
                MauiApp mauiApp = builder.Build();
                _mauiContext = new MauiContext(mauiApp.Services);

                m_window = new MainWindow(_mauiContext);
                m_window.Activate();
            }
            else
            {
                // TODO: do things on subsequent launches, like processing arguments from e.Arguments
            }
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _singleInstanceApp.Launch(args.Arguments);
        }

        private Microsoft.UI.Xaml.Window m_window;
    }
}
