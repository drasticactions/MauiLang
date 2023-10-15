using Drastic.Tools;
using MauiLang.Services;
using MauiLang.Translations;

namespace MauiLang.ViewModels;

public class TranslationViewModel : MauiLangViewModel
{
    private TranslationResult? result;
    private string inputText = string.Empty;

    public TranslationViewModel(IServiceProvider services)
        : base(services)
    {
        this.TranslateCommand = new AsyncCommand(this.TranslateAsync, () => !string.IsNullOrEmpty(this.InputText), this.Dispatcher, this.ErrorHandler);
        this.OpenExtraCommand = new AsyncCommand(this.OpenExtraAsync, () => this.Result is not null, this.Dispatcher, this.ErrorHandler);
    }
    
    public AsyncCommand TranslateCommand { get; }

    public AsyncCommand OpenExtraCommand { get; }
    
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

        this.Result = await this.OpenAI.GenerateTextAsync(this.InputText);
       // this.OutputText = result.translation;
    }

    public async Task OpenExtraAsync()
    {
       Application.Current?.MainPage.DisplayAlert(Common.ExplainLabel, this.Result?.explain ?? "No explanation found.", "Ok");
    }
}