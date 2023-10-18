// <copyright file="SettingsPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

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
    
    private void PopModalTapped(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
    
    private void OutputLanguageTapped(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(this.outputPage);
    }
}