// <copyright file="SettingsViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tools;
using Drastic.ViewModels;
using MauiLang.Models;

namespace MauiLang.ViewModels;

/// <summary>
/// Settings View Model.
/// </summary>
public class SettingsViewModel : MauiLangViewModel
{
    private string openAIToken;
    private MauiLangLanguage outputResponseLanguage;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
    /// </summary>
    /// <param name="services">The service provider.</param>
    public SettingsViewModel(IServiceProvider services)
        : base(services)
    {
        this.openAIToken = this.Settings.OpenAIToken ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets the OpenAI token.
    /// </summary>
    public string OpenAIToken
    {
        get => this.openAIToken;
        set
        {
            this.SetProperty(ref this.openAIToken, value);
            this.Settings.OpenAIToken = value;
            this.Database.SetSettings(this.Settings);
        }
    }
}