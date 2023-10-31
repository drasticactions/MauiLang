using Drastic.Tools;
using Microsoft.Maui.Platform;


namespace MauiLang;

public partial class MainPage : ContentPage
{
    private IServiceProvider provider;
    private IModalNavigation parent;
    private ViewModels.TranslationViewModel vm;

    public MainPage(IServiceProvider provider, IModalNavigation parent)
    {
        InitializeComponent();
#if MACCATALYST || IOS
        this.TopEditor.FontFamily = "Helvetica Neue";
        this.TopEditor.FontSize = 18;
        this.TopEditor.FontAttributes = FontAttributes.Bold;

        this.BottomEditor.FontFamily = "Helvetica Neue";
        this.BottomEditor.FontSize = 18;
        this.BottomEditor.FontAttributes = FontAttributes.Bold;
#endif
        this.provider = provider;
        this.parent = parent;
        this.BindingContext = this.vm = this.provider.ResolveWith<ViewModels.TranslationViewModel>();
    }

    /// <inheritdoc/>
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
#if MACCATALYST
        this.SettingsButton.Text = string.Empty;
        var gearIcon = UIKit.UIImage.GetSystemImage("gearshape");
        var button = (UIKit.UIButton)this.SettingsButton.Handler!.PlatformView;
        button!.PreferredBehavioralStyle = UIKit.UIBehavioralStyle.Pad;
        button!.SetImage(gearIcon, UIKit.UIControlState.Normal);
        var languageLabel = (UIKit.UILabel)this.LanguageSelectionLabel.Handler!.PlatformView;
        var tapGesture = new UIKit.UITapGestureRecognizer(HandleTap);
        languageLabel.UserInteractionEnabled = true;
        languageLabel.AddGestureRecognizer(tapGesture);
#elif WINDOWS
        this.SettingsButton.Text = string.Empty;
        var gearIcon = Microsoft.UI.Xaml.Controls.Symbol.Setting;
        var button = (Microsoft.UI.Xaml.Controls.Button)this.SettingsButton.Handler!.PlatformView;
        button!.Content = new Microsoft.UI.Xaml.Controls.SymbolIcon() { Symbol = gearIcon };

        var languageLabel = (Microsoft.UI.Xaml.Controls.TextBlock)this.LanguageSelectionLabel.Handler!.PlatformView;
        languageLabel.Tapped += LanguageLabel_Tapped;
#endif
    }

#if WINDOWS
    private void LanguageLabel_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        this.parent.OpenLanguageSelectionModal();
    }
#endif

    #if MACCATALYST
    void HandleTap(UIKit.UITapGestureRecognizer recognizer)
    {
        this.parent.OpenLanguageSelectionModal();
    }
    #endif

    private void SettingsButton_OnClicked(object sender, EventArgs e)
    {
        this.parent.OpenSettingsModal();
    }
}