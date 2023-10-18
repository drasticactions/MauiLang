// <copyright file="TargetLanguageViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tools;
using MauiLang.Models;

namespace MauiLang.ViewModels;

public class TargetLanguageViewModel : MauiLangViewModel
{
    private TranslationViewModel vm;

    public TargetLanguageViewModel(IServiceProvider services)
        : base(services)
    {
        this.vm = services.GetRequiredService<TranslationViewModel>();
        this.Languages = MauiLangLanguage.GenerateMauiLangLangauages();
        this.SelectLanguageCommand =
            new AsyncCommand<MauiLangLanguage>(this.SelectLanguageAsync, null, this.ErrorHandler);
    }

    public IReadOnlyList<MauiLangLanguage> Languages { get; }

    public AsyncCommand<MauiLangLanguage> SelectLanguageCommand { get; }

    public async Task SelectLanguageAsync(MauiLangLanguage language)
    {
        this.vm.TargetLanguage = language;
    }
}