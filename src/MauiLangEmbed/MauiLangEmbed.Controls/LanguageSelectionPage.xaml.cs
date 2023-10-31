using Drastic.Tools;
using MauiLang.Models;
using MauiLang.ViewModels;

namespace MauiLang;

public partial class LanguageSelectionPage : ContentPage
{
    private TargetLanguageViewModel vm;
    private IModalNavigation? parent;

    public LanguageSelectionPage(IServiceProvider provider, IModalNavigation? parent = default)
    {
        InitializeComponent();
        this.parent = parent;
        this.BindingContext = this.vm = provider.ResolveWith<TargetLanguageViewModel>();
    }

    private void CloseButtonClicked(object sender, EventArgs e)
    {
        this.parent.CloseModal();
    }

    private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is MauiLangLanguage lang)
        {
            await this.vm.SelectLanguageCommand.ExecuteAsync(lang);
            this.parent.CloseModal();
            this.ListView.SelectedItem = null;
        }
    }
}