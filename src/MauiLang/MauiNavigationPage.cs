// <copyright file="MauiNavigationPage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace MauiLang;

/// <summary>
/// Maui Navigation Page.
/// </summary>
public class MauiNavigationPage : NavigationPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MauiNavigationPage"/> class.
    /// </summary>
    /// <param name="page">Page.</param>
    public MauiNavigationPage(Page page)
        : base(page)
    {
        this.Title = Translations.Common.AppName;
    }
}