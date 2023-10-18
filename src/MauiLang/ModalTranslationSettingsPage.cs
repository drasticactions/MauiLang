// <copyright file="ModalTranslationSettingsPage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace MauiLang;

public class ModalTranslationSettingsPage : Microsoft.Maui.Controls.NavigationPage
{
    public ModalTranslationSettingsPage(IServiceProvider provider)
        : base(provider.GetRequiredService<LanguageSelectionPage>())
    {
        this.On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.FormSheet);
    }
}