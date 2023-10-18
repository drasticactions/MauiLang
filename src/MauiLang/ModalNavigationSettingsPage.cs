// <copyright file="ModalNavigationSettingsPage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace MauiLang;

public class ModalNavigationSettingsPage : Microsoft.Maui.Controls.NavigationPage
{
    public ModalNavigationSettingsPage(IServiceProvider provider)
        : base(provider.GetRequiredService<SettingsPage>())
    {
        this.On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.FormSheet);
    }
}