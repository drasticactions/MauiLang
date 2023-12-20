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
    
    private async void ListView_OnOnSelectedItemsChanged(object? sender, SelectedItemsChangedEventArgs e)
    {
        var position = e.NewSelection.First();
        var item = this.vm.Languages.GetItem(position.SectionIndex, position.ItemIndex);
        if (item is not null)
        {
            await this.vm.SelectLanguageCommand.ExecuteAsync(item);
            this.closeAction?.Invoke();
            this.ListView.SelectedItem = null;
        }
    }
}