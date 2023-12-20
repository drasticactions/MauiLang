using Drastic.Tools;
using MauiLang.Models;
using MauiLang.ViewModels;

namespace MauiLang;

public partial class LanguageSelectionPage : ContentPage
{
    private TargetLanguageViewModel vm;

    private Action closeAction;

    public LanguageSelectionPage(IServiceProvider provider, Action closeAction)
    {
        InitializeComponent();
        this.closeAction = closeAction;
        this.BindingContext = this.vm = provider.ResolveWith<TargetLanguageViewModel>();
    }

    private void CloseButtonClicked(object sender, EventArgs e)
    {
        this.closeAction?.Invoke();
    }

    private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is MauiLangLanguage lang)
        {
            await this.vm.SelectLanguageCommand.ExecuteAsync(lang);
            this.closeAction?.Invoke();
            this.ListView.SelectedItem = null;
        }
    }
}