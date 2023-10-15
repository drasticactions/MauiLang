using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiLang.Models;
using MauiLang.ViewModels;
using Microsoft.Maui.Handlers;

namespace MauiLang;

public partial class LanguageSelectionPage : ContentPage
{
    private TargetLanguageViewModel vm;
    
    public LanguageSelectionPage(IServiceProvider provider)
    {
        InitializeComponent();
        this.BindingContext = this.vm = provider.GetRequiredService<TargetLanguageViewModel>();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is MauiLangLanguage lang)
        {
            await this.vm.SelectLanguageCommand.ExecuteAsync(lang);
            await Navigation.PopModalAsync();
            this.ListView.SelectedItem = null;
        }
    }
}