using MauiLang.ViewModels;

namespace MauiLang;

public partial class MainPage : ContentPage
{
	private TranslationViewModel vm;
	private IServiceProvider provider;
	public MainPage(IServiceProvider provider)
	{
		InitializeComponent();
		this.provider = provider;
		#if MACCATALYST || IOS
		this.TopEditor.FontFamily = "Helvetica Neue";
		this.TopEditor.FontSize = 18;
		this.TopEditor.FontAttributes = FontAttributes.Bold;
		
		this.BottomEditor.FontFamily = "Helvetica Neue";
		this.BottomEditor.FontSize = 18;
		this.BottomEditor.FontAttributes = FontAttributes.Bold;
		#endif
		
		this.BindingContext = this.vm = new TranslationViewModel(provider);
	}

	protected override void OnHandlerChanged()
	{
		base.OnHandlerChanged();
#if MACCATALYST
		this.SettingsButton.IsVisible = false;
#elif IOS
		this.SettingsButton.Text = string.Empty;
		var gearIcon = UIKit.UIImage.GetSystemImage("gearshape");
		var button = (UIKit.UIButton)this.SettingsButton.Handler!.PlatformView;
		button!.SetImage(gearIcon, UIKit.UIControlState.Normal);
#endif
	}

	private void SettingsButton_OnClicked(object sender, EventArgs e)
	{
		Navigation.PushModalAsync(this.provider.GetRequiredService<ModalNavigationSettingsPage>());
	}
}

