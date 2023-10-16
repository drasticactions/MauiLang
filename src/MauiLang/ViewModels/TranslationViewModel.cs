using Drastic.Tools;
using MauiLang.Models;
using MauiLang.Services;
using MauiLang.Translations;

namespace MauiLang.ViewModels;

public class TranslationViewModel : MauiLangViewModel
{
    private TranslationResult? result;
    private string inputText = string.Empty;
    private MauiLangLanguage targetLanguage;
    
    public TranslationViewModel(IServiceProvider services)
        : base(services)
    {
        this.targetLanguage = this.Settings.TargetLanguage ?? new MauiLangLanguage();
        this.TranslateCommand = new AsyncCommand(this.TranslateAsync, () => !this.IsBusy && !string.IsNullOrEmpty(this.InputText), this.Dispatcher, this.ErrorHandler);
        this.OpenExtraCommand = new AsyncCommand(this.OpenExtraAsync, () => !this.IsBusy && this.Result is not null, this.Dispatcher, this.ErrorHandler);
    }
    
    public AsyncCommand TranslateCommand { get; }

    public AsyncCommand OpenExtraCommand { get; }
    
    public MauiLangLanguage TargetLanguage
    {
        get => this.targetLanguage;
        set
        {
            this.SetProperty(ref this.targetLanguage, value);
            this.Settings.TargetLanguage = value;
            this.Database.SetSettings(this.Settings);
        }
    }
    
    public string InputText
    {
        get => this.inputText;
        set
        {
            this.SetProperty(ref this.inputText, value);
            this.RaiseCanExecuteChanged();
        }
    }

    public TranslationResult? Result {
        get => this.result;
        set {
            this.SetProperty(ref this.result, value);
            this.RaiseCanExecuteChanged();
        }
    }

    public override void RaiseCanExecuteChanged()
    {
        base.RaiseCanExecuteChanged();
        this.TranslateCommand.RaiseCanExecuteChanged();
        this.OpenExtraCommand.RaiseCanExecuteChanged();
    }

    public async Task TranslateAsync()
    {
        if (string.IsNullOrWhiteSpace(this.InputText))
            return;

        this.Result = null;
        this.IsBusy = true;
        this.RaiseCanExecuteChanged();
        this.Result = await this.OpenAI.GenerateTextAsync(this.InputText);
        this.IsBusy = false;
        this.RaiseCanExecuteChanged();
    }

    public async Task OpenExtraAsync()
    {
        if (this.Result is null)
            return;

        this.IsBusy = true;
        this.RaiseCanExecuteChanged();
        this.Result = await this.OpenAI.GenerateExplainAsync(this.Result);
        this.IsBusy = false;
        this.RaiseCanExecuteChanged(); 
        await Application.Current!.MainPage!.DisplayAlert(Common.ExplainLabel, this.Result?.explain ?? Common.NoExplainLabel, "Ok");
    }
}