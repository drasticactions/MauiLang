using Drastic.ViewModels;
using MauiLang.Models;

namespace MauiLang.ViewModels;

public class SettingsViewModel : MauiLangViewModel
{
    private string openAIToken = string.Empty;
    private MauiLangLanguage targetLanguage;
    private MauiLangLanguage outputResponseLanguage;
    
    public SettingsViewModel(IServiceProvider services)
        : base(services)
    {
        this.openAIToken = this.Settings.OpenAIToken;
        this.targetLanguage = this.Settings.TargetLanguage ?? new MauiLangLanguage();
        this.outputResponseLanguage = this.Settings.OutputResponseLanguage ?? new MauiLangLanguage();
    }
    
    public string OpenAIToken
    {
        get => this.openAIToken;
        set
        {
            this.SetProperty(ref this.openAIToken, value);
            this.Settings.OpenAIToken = value;
            this.Database.SetSettings(this.Settings);
        }
    }
    
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
    
    public MauiLangLanguage OutputResponseLanguage
    {
        get => this.outputResponseLanguage;
        set
        {
            this.SetProperty(ref this.outputResponseLanguage, value);
            this.Settings.OutputResponseLanguage = value;
            this.Database.SetSettings(this.Settings);
        }
    }
}