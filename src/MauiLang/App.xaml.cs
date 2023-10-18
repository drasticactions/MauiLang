// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace MauiLang;

/// <summary>
/// Maui App.
/// </summary>
public partial class App : Application
{
    private IServiceProvider provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    /// <param name="provider">Provider.</param>
    public App(IServiceProvider provider)
    {
        this.InitializeComponent();

        this.provider = provider;
    }

    /// <inheritdoc/>
    protected override Window CreateWindow(IActivationState activationState)
    {
        if (activationState is null)
        {
            throw new ArgumentNullException(nameof(activationState));
        }

        return new Window(new MauiNavigationPage(new MainPage(this.provider)));
    }
}