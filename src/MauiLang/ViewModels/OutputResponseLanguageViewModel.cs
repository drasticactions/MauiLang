using Drastic.Tools;
using MauiLang.Models;

namespace MauiLang.ViewModels;

public class OutputResponseLanguageViewModel : MauiLangViewModel
{
    private SettingsViewModel vm;
    
    public OutputResponseLanguageViewModel(IServiceProvider services)
        : base(services)
    {
        this.vm = services.GetRequiredService<SettingsViewModel>();
        this.Languages = MauiLangLanguage.GenerateMauiLangLangauages();
        this.SelectLanguageCommand =
            new AsyncCommand<MauiLangLanguage>(this.SelectLanguageAsync, null, this.ErrorHandler);
    }
    
    public IReadOnlyList<MauiLangLanguage> Languages { get; }
    
    public AsyncCommand<MauiLangLanguage> SelectLanguageCommand { get; }

    public async Task SelectLanguageAsync(MauiLangLanguage language)
    {
        this.vm.OutputResponseLanguage = language;
    }
}