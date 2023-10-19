// <copyright file="TranslationViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;
using Drastic.Tools;
using MauiLang.Models;
using MauiLang.Services;
using MauiLang.Translations;

namespace MauiLang.ViewModels;

public class TranslationViewModel : MauiLangViewModel, IErrorHandlerService
{
    private TranslationResult? result;
    private string inputText = string.Empty;
    private MauiLangLanguage targetLanguage;

    /// <summary>
    /// Initializes a new instance of the <see cref="TranslationViewModel"/> class.
    /// </summary>
    /// <param name="services"></param>
    public TranslationViewModel(IServiceProvider services)
        : base(services)
    {
        this.targetLanguage = this.Settings.TargetLanguage ?? new MauiLangLanguage();
        this.TranslateCommand = new AsyncCommand(this.TranslateAsync, () => !this.IsBusy && !string.IsNullOrEmpty(this.InputText), this.Dispatcher, this);
        this.OpenExtraCommand = new AsyncCommand(this.OpenExtraAsync, () => !this.IsBusy && this.Result is not null, this.Dispatcher, this);
    }

    /// <summary>
    /// Gets the command to translate the input text.
    /// </summary>
    public AsyncCommand TranslateCommand { get; }

    /// <summary>
    /// Gets the command to open the extra information about the translation result.
    /// </summary>
    public AsyncCommand OpenExtraCommand { get; }

    /// <summary>
    /// Gets or sets the target language for translation.
    /// </summary>
    public MauiLangLanguage TargetLanguage
    {
        get => this.targetLanguage;
        set
        {
            this.SetProperty(ref this.targetLanguage, value);

            // Save the target language to the app settings.
            this.Settings.TargetLanguage = value;

            // Save the app settings to the database.
            this.Database.SetSettings(this.Settings);
        }
    }

    /// <summary>
    /// Gets or sets the input text to be translated.
    /// </summary>
    public string InputText
    {
        get => this.inputText;
        set
        {
            this.SetProperty(ref this.inputText, value);
            this.RaiseCanExecuteChanged();
        }
    }

    /// <summary>
    /// Gets or sets the result of the translation.
    /// </summary>
    public TranslationResult? Result
    {
        get => this.result;
        set
        {
            this.SetProperty(ref this.result, value);
            this.RaiseCanExecuteChanged();
        }
    }

    /// <inheritdoc/>
    public override void RaiseCanExecuteChanged()
    {
        base.RaiseCanExecuteChanged();
        this.TranslateCommand.RaiseCanExecuteChanged();
        this.OpenExtraCommand.RaiseCanExecuteChanged();
    }

    /// <summary>
    /// Handles an error by displaying an alert with the error message.
    /// </summary>
    /// <param name="ex">The exception to handle.</param>
    void IErrorHandlerService.HandleError(Exception ex)
    {
        this.IsBusy = false;
        this.RaiseCanExecuteChanged();
        this.Result = null;
        Application.Current?.MainPage?.DisplayAlert("Error", ex.Message, "Ok");
    }

    /// <summary>
    /// Translates the input text.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    private async Task TranslateAsync()
    {
        if (string.IsNullOrWhiteSpace(this.InputText))
        {
            return;
        }

        this.Result = null;
        this.IsBusy = true;
        this.RaiseCanExecuteChanged();
        this.Result = await this.OpenAI.GenerateTextAsync(this.InputText);
        this.IsBusy = false;
        this.RaiseCanExecuteChanged();
    }

    /// <summary>
    /// Opens the extra information about the translation result.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    private async Task OpenExtraAsync()
    {
        if (this.Result is null)
        {
            return;
        }

        this.IsBusy = true;
        this.RaiseCanExecuteChanged();
        this.Result = await this.OpenAI.GenerateExplainAsync(this.Result);
        this.IsBusy = false;
        this.RaiseCanExecuteChanged();
        await Application.Current!.MainPage!.DisplayAlert(Common.ExplainLabel, this.Result?.Explain ?? Common.NoExplainLabel, "Ok");
    }
}