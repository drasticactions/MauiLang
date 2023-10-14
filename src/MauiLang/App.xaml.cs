namespace MauiLang;

public partial class App : Application
{
	public App(IServiceProvider provider)
	{
		InitializeComponent();

		MainPage = new NavigationPage(new MainPage(provider));
	}
}