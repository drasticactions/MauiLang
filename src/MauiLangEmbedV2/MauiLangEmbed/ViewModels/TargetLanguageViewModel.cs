// <copyright file="TargetLanguageViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tools;
using MauiLang.Models;
using Microsoft.Maui.Adapters;

namespace MauiLang.ViewModels;

/// <summary>
/// Target Language View Model.
/// </summary>
public class TargetLanguageViewModel : MauiLangViewModel
{
    private readonly TranslationViewModel vm;

    /// <summary>
    /// Initializes a new instance of the <see cref="TargetLanguageViewModel"/> class.
    /// </summary>
    /// <param name="services">The service provider.</param>
    public TargetLanguageViewModel(IServiceProvider services)
        : base(services)
    {
        this.vm = services.GetRequiredService<TranslationViewModel>();
        this.Languages = new VirtualListViewAdapter<MauiLangLanguage>(MauiLangLanguage.GenerateMauiLangLangauages());
        this.SelectLanguageCommand =
            new AsyncCommand<MauiLangLanguage>(this.SelectLanguageAsync, null, this.ErrorHandler);
    }

    /// <summary>
    /// Gets the list of available languages.
    /// </summary>
    public VirtualListViewAdapter<MauiLangLanguage> Languages { get; }

    /// <summary>
    /// Gets the command to select a language.
    /// </summary>
    public AsyncCommand<MauiLangLanguage> SelectLanguageCommand { get; }

    /// <summary>
    /// Selects a target language.
    /// </summary>
    /// <param name="language">The language to select.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    private Task SelectLanguageAsync(MauiLangLanguage language)
    {
        this.vm.TargetLanguage = language;
        return Task.CompletedTask;
    }
}