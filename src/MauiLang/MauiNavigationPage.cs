// <copyright file="MauiNavigationPage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace MauiLang;

public class MauiNavigationPage : NavigationPage
{
    public MauiNavigationPage(Page page) : base(page)
    {
        this.Title = Translations.Common.AppName;
    }
}