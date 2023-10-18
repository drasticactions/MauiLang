// <copyright file="ModalTranslationSettingsPage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace MauiLang;

/// <summary>
/// Modal Navigation Settings Page.
/// </summary>
public class ModalTranslationSettingsPage : Microsoft.Maui.Controls.NavigationPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModalTranslationSettingsPage"/> class.
    /// </summary>
    /// <param name="provider">Provider.</param>
    public ModalTranslationSettingsPage(IServiceProvider provider)
        : base(provider.GetRequiredService<LanguageSelectionPage>())
    {
        this.On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.FormSheet);
    }
}