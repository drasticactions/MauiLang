// <copyright file="TranslationResult.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace MauiLang.Services;

/// <summary>
/// Translation Result.
/// </summary>
public class TranslationResult
{
    /// <summary>
    /// Gets or sets the translation.
    /// </summary>
    public string? Translation { get; set; }

    /// <summary>
    /// Gets or sets the explain.
    /// </summary>
    public string? Explain { get; set; }

    /// <summary>
    /// Gets or sets the input text.
    /// </summary>
    public string? InputText { get; set; }

    /// <summary>
    /// Gets or sets the translate from.
    /// </summary>
    public string? TranslateTo { get; set; }

    /// <summary>
    /// Gets or sets the translate to.
    /// </summary>
    public string? RespondIn { get; set; }
}