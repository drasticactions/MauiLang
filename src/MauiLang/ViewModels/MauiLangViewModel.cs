// <copyright file="MauiLangViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.ViewModels;
using MauiLang.Services;

namespace MauiLang.ViewModels;

/// <summary>
/// MauiLang View Model.
/// </summary>
public class MauiLangViewModel : BaseViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MauiLangViewModel"/> class.
    /// </summary>
    /// <param name="services">The services.</param>
    protected MauiLangViewModel(IServiceProvider services)
        : base(services)
    {
        this.Database = services.GetRequiredService<DatabaseService>();
        this.OpenAI = services.GetRequiredService<OpenAIService>();
        this.Settings = services.GetRequiredService<Settings>();
    }

    /// <summary>
    /// Gets the database service.
    /// </summary>
    protected DatabaseService Database { get; }

    /// <summary>
    /// Gets the OpenAI Service.
    /// </summary>
    protected OpenAIService OpenAI { get; }

    /// <summary>
    /// Gets the settings.
    /// </summary>
    protected Settings Settings { get; }
}