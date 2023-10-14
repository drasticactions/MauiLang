using Drastic.Tools;

namespace MauiLang.ViewModels;

public class TranslationViewModel : MauiLangViewModel
{
    private string inputText = string.Empty;
    private string outputText = string.Empty;
    public TranslationViewModel(IServiceProvider services)
        : base(services)
    {
        this.TranslateCommand = new AsyncCommand(this.TranslateAsync, () => !string.IsNullOrEmpty(this.InputText), this.Dispatcher, this.ErrorHandler);
    }
    
    public AsyncCommand TranslateCommand { get; }
    
    public string InputText
    {
        get => this.inputText;
        set
        {
            this.SetProperty(ref this.inputText, value);
            this.RaiseCanExecuteChanged();
        }
    }
    
    public string OutputText
    {
        get => this.outputText;
        set => this.SetProperty(ref this.outputText, value);
    }

    public override void RaiseCanExecuteChanged()
    {
        base.RaiseCanExecuteChanged();
        this.TranslateCommand.RaiseCanExecuteChanged();
    }

    public async Task TranslateAsync()
    {
        if (string.IsNullOrWhiteSpace(this.InputText))
            return;

        var result = await this.OpenAI.GenerateTextAsync(this.InputText);
        this.OutputText = result.translation;
    }
}