// <copyright file="OutputResponseLanguagePage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiLang.Models;
using MauiLang.ViewModels;

namespace MauiLang;

/// <summary>
/// Output Response Language Page.
/// </summary>
public partial class OutputResponseLanguagePage : ContentPage
{
    private OutputResponseLanguageViewModel vm;

    /// <summary>
    /// Initializes a new instance of the <see cref="OutputResponseLanguagePage"/> class.
    /// </summary>
    /// <param name="provider">Provider.</param>
    public OutputResponseLanguagePage(IServiceProvider provider)
    {
        this.InitializeComponent();
        this.BindingContext = this.vm = provider.GetRequiredService<OutputResponseLanguageViewModel>();
    }

    private void BackButtonPressed(object sender, EventArgs e)
    {
        this.Navigation.PopAsync();
    }

    private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is MauiLangLanguage lang)
        {
            await this.vm.SelectLanguageCommand.ExecuteAsync(lang);
            await this.Navigation.PopAsync();
        }
    }
}