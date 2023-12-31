﻿// <copyright file="MainPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using MauiLang.ViewModels;

namespace MauiLang;

/// <summary>
/// Main Page.
/// </summary>
public partial class MainPage : ContentPage
{
    private TranslationViewModel vm;
    private IServiceProvider provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    /// <param name="provider">Provider.</param>
    public MainPage(IServiceProvider provider)
    {
        this.InitializeComponent();
        this.provider = provider;
#if MACCATALYST || IOS
        this.TopEditor.FontFamily = "Helvetica Neue";
        this.TopEditor.FontSize = 18;
        this.TopEditor.FontAttributes = FontAttributes.Bold;

        this.BottomEditor.FontFamily = "Helvetica Neue";
        this.BottomEditor.FontSize = 18;
        this.BottomEditor.FontAttributes = FontAttributes.Bold;
#endif

        this.BindingContext = this.vm = provider.GetRequiredService<TranslationViewModel>();
    }

    /// <inheritdoc/>
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
#if MACCATALYST
        this.SettingsRow.IsVisible = false;
#elif IOS
        this.SettingsButton.Text = string.Empty;
        var gearIcon = UIKit.UIImage.GetSystemImage("gearshape");
        var button = (UIKit.UIButton)this.SettingsButton.Handler!.PlatformView;
        button!.SetImage(gearIcon, UIKit.UIControlState.Normal);
#elif WINDOWS
        this.SettingsButton.Text = string.Empty;
        var gearIcon = Microsoft.UI.Xaml.Controls.Symbol.Setting;
        var button = (Microsoft.UI.Xaml.Controls.Button)this.SettingsButton.Handler!.PlatformView;
        button!.Content = new Microsoft.UI.Xaml.Controls.SymbolIcon() { Symbol = gearIcon };
#endif
    }

    private void SettingsButton_OnClicked(object sender, EventArgs e)
    {
        this.Navigation.PushModalAsync(this.provider.GetRequiredService<ModalNavigationSettingsPage>());
    }

    private void TapGestureRecognizer_OnTapped(object sender, TappedEventArgs e)
    {
        this.Navigation.PushModalAsync(this.provider.GetRequiredService<ModalTranslationSettingsPage>());
    }
}