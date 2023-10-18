// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace MauiLang;

public partial class App : Application
{
	private IServiceProvider provider;

	public App(IServiceProvider provider)
	{
		this.InitializeComponent();

		this.provider = provider;
	}

	protected override Window CreateWindow(IActivationState activationState)
	{
		return new Window(new MauiNavigationPage(new MainPage(this.provider)));
	}
}