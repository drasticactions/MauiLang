using MauiLang.ViewModels;

namespace MauiLang;

public partial class MainPage : ContentPage
{
	private TranslationViewModel vm;
	private IServiceProvider provider;
	private SettingsPage settings;
	public MainPage(IServiceProvider provider)
	{
		InitializeComponent();
		this.provider = provider;
		this.settings = new SettingsPage(provider);
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
#if MACCATALYST || IOS
		this.SettingsButton.Text = string.Empty;
		var gearIcon = UIKit.UIImage.GetSystemImage("gearshape");
		var button = (UIKit.UIButton)this.SettingsButton.Handler!.PlatformView;
		button!.SetImage(gearIcon, UIKit.UIControlState.Normal);
		//var translationButton = (UIKit.UIButton)this.TranslateButton.Handler!.PlatformView;
		//translationButton!.Layer.MasksToBounds = true;
		//translationButton.Layer.CornerRadius = (System.Runtime.InteropServices.NFloat)(button.Frame.Width / 2);
#endif
	}

	private void SettingsButton_OnClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(settings);
	}
}

