// <copyright file="SettingsViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.ViewModels;
using MauiLang.Models;

namespace MauiLang.ViewModels;

/// <summary>
/// Settings View Model.
/// </summary>
public class SettingsViewModel : MauiLangViewModel
{
    private string openAIToken = string.Empty;
    private MauiLangLanguage outputResponseLanguage;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
    /// </summary>
    /// <param name="services">The service provider.</param>
    public SettingsViewModel(IServiceProvider services)
        : base(services)
    {
        this.openAIToken = this.Settings.OpenAIToken;
        this.outputResponseLanguage = this.Settings.OutputResponseLanguage ?? new MauiLangLanguage();
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

    /// <summary>
    /// Gets or sets the output response language.
    /// </summary>
    public MauiLangLanguage OutputResponseLanguage
    {
        get => this.outputResponseLanguage;
        set
        {
            this.SetProperty(ref this.outputResponseLanguage, value);
            this.Settings.OutputResponseLanguage = value;
            this.Database.SetSettings(this.Settings);
        }
    }
}