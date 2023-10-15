using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiLang.ViewModels;

namespace MauiLang;

public partial class SettingsPage : ContentPage
{
    private IServiceProvider provider;
    private SettingsViewModel vm;
    private LanguageSelectionPage page;
    private OutputResponseLanguagePage outputPage;
    
    public SettingsPage(IServiceProvider provider)
    {
        InitializeComponent();
        this.provider = provider;
        this.page = new LanguageSelectionPage(provider);
        this.outputPage = new OutputResponseLanguagePage(provider);
        this.BindingContext = this.vm = this.provider.GetRequiredService<SettingsViewModel>();
    }
    
    private void Button_OnClicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void Cell_OnTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(this.page);
    }

    private void Cell2_OnTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(this.outputPage);
    }

    private void TapGestureRecognizer_OnTapped(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(this.page);
    }
    
    private void TapGestureRecognizer2_OnTapped(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(this.outputPage);
    }
}