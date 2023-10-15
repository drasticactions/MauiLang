namespace MauiLang;

public partial class App : Application
{
	public App(IServiceProvider provider)
	{
		InitializeComponent();

		MainPage = new MauiNavigationPage(new MainPage(provider));
	}
}

public class MauiNavigationPage : NavigationPage
{
	public MauiNavigationPage(Page page) : base(page)
	{
		
	}

	protected override void OnHandlerChanged()
	{
		base.OnHandlerChanged();
	}
}