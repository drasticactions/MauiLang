// <copyright file="OutputResponseLanguageViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tools;
using MauiLang.Models;

namespace MauiLang.ViewModels;

/// <summary>
/// Output Response Language View Model.
/// </summary>
public class OutputResponseLanguageViewModel : MauiLangViewModel
{
    private readonly SettingsViewModel vm;

    /// <summary>
    /// Initializes a new instance of the <see cref="OutputResponseLanguageViewModel"/> class.
    /// </summary>
    /// <param name="services">The service provider.</param>
    public OutputResponseLanguageViewModel(IServiceProvider services)
        : base(services)
    {
        this.vm = services.GetRequiredService<SettingsViewModel>();
        this.Languages = MauiLangLanguage.GenerateMauiLangLangauages();
        this.SelectLanguageCommand =
            new AsyncCommand<MauiLangLanguage>(this.SelectLanguageAsync, null, this.ErrorHandler);
    }

    /// <summary>
    /// Gets the list of available languages.
    /// </summary>
    public IReadOnlyList<MauiLangLanguage> Languages { get; }

    /// <summary>
    /// Gets the command to select a language.
    /// </summary>
    public AsyncCommand<MauiLangLanguage> SelectLanguageCommand { get; }

    /// <summary>
    /// Selects the specified language.
    /// </summary>
    /// <param name="language">The language to select.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    private Task SelectLanguageAsync(MauiLangLanguage language)
    {
        this.vm.OutputResponseLanguage = language;
        return Task.CompletedTask;
    }
}