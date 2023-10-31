using Drastic.Tools;
using MauiLang.Models;
using MauiLang.ViewModels;

namespace MauiLang;

public partial class OutputResponseLanguagePage : ContentPage
{
    private OutputResponseLanguageViewModel vm;
    private INativeNavigation? parent;
    private IServiceProvider provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="OutputResponseLanguagePage"/> class.
    /// </summary>
    /// <param name="provider">Provider.</param>
    public OutputResponseLanguagePage(IServiceProvider provider, INativeNavigation? parent = default)
    {
        this.InitializeComponent();
        this.provider = provider;
        this.parent = parent;
        this.BindingContext = this.vm = provider.ResolveWith<OutputResponseLanguageViewModel>();
    }

    private void BackButtonPressed(object sender, EventArgs e)
    {
        this.parent?.SetPage(this.provider.ResolveWith<SettingsPage>(this.parent));
    }

    private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is MauiLangLanguage lang)
        {
            await this.vm.SelectLanguageCommand.ExecuteAsync(lang);
            this.parent?.SetPage(this.provider.ResolveWith<SettingsPage>(this.parent));
        }
    }
}