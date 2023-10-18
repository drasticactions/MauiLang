// <copyright file="LanguageSelectionPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

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

    /// <summary>
    /// Initializes a new instance of the <see cref="LanguageSelectionPage"/> class.
    /// </summary>
    /// <param name="provider">Provider.</param>
    public LanguageSelectionPage(IServiceProvider provider)
    {
        this.InitializeComponent();
        this.BindingContext = this.vm = provider.GetRequiredService<TargetLanguageViewModel>();
    }

    private void CloseButtonClicked(object sender, EventArgs e)
    {
        this.Navigation.PopModalAsync();
    }

    private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is MauiLangLanguage lang)
        {
            await this.vm.SelectLanguageCommand.ExecuteAsync(lang);
            await this.Navigation.PopModalAsync();
            this.ListView.SelectedItem = null;
        }
    }
}