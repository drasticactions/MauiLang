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
    private OutputResponseLanguagePage outputPage;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsPage"/> class.
    /// </summary>
    /// <param name="provider">Provider.</param>
    public SettingsPage(IServiceProvider provider)
    {
        this.InitializeComponent();
        this.provider = provider;
        this.outputPage = this.provider.GetRequiredService<OutputResponseLanguagePage>();
        this.BindingContext = this.vm = this.provider.GetRequiredService<SettingsViewModel>();
    }

    private void PopModalTapped(object sender, EventArgs e)
    {
        this.Navigation.PopModalAsync();
    }

    private void OutputLanguageTapped(object sender, TappedEventArgs e)
    {
        this.Navigation.PushAsync(this.outputPage);
    }
}