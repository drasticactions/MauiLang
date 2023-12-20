// <copyright file="Settings.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using MauiLang.Models;

namespace MauiLang.Services;

/// <summary>
/// Settings.
/// </summary>
public class Settings
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the OpenAI token.
    /// </summary>
    public string? OpenAIToken { get; set; }

    /// <summary>
    /// Gets or sets the target language.
    /// </summary>
    public MauiLangLanguage? TargetLanguage { get; set; }

    /// <summary>
    /// Gets or sets the output response language.
    /// </summary>
    public MauiLangLanguage? OutputResponseLanguage { get; set; }
}