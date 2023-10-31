using Drastic.Tools;

namespace MauiLang;

public partial class MainPage : ContentPage
{
    private IServiceProvider provider;
    private INativeNavigation parent;
    private ViewModels.TranslationViewModel vm;

    public MainPage(IServiceProvider provider, INativeNavigation parent)
    {
        InitializeComponent();
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
        button!.SetImage(gearIcon, UIKit.UIControlState.Normal);
#elif WINDOWS
        this.SettingsButton.Text = string.Empty;
        var gearIcon = Microsoft.UI.Xaml.Controls.Symbol.Setting;
        var button = (Microsoft.UI.Xaml.Controls.Button)this.SettingsButton.Handler!.PlatformView;
        button!.Content = new Microsoft.UI.Xaml.Controls.SymbolIcon() { Symbol = gearIcon };
#endif
    }

    private void SettingsButton_OnClicked(object sender, EventArgs e)
    {
        this.parent.OpenModal();
    }
}