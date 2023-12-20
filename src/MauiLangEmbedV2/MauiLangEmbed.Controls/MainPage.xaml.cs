using Drastic.Tools;
using MauiLang.Services;
using Microsoft.Maui.Platform;


namespace MauiLang;

public partial class MainPage : ContentPage
{
    private IServiceProvider provider;
    private ViewModels.TranslationViewModel vm;
    private Action onLanguageSelection;
    private DatabaseService db;
    
    public MainPage(IServiceProvider provider, Action onLanguageSelection)
    {
        this.InitializeComponent();
        this.db = provider.GetRequiredService<DatabaseService>();
        this.db.SettingsChanged += (sender, args) =>
        {
            // Why does this not work with bindings???
            this.LanguageSelectionLabel.Text = args.Settings.TargetLanguage?.Language ?? string.Empty;
        };
        this.onLanguageSelection = onLanguageSelection;
#if MACCATALYST || IOS
        this.TopEditor.FontFamily = "Helvetica Neue";
        this.TopEditor.FontSize = 18;
        this.TopEditor.FontAttributes = FontAttributes.Bold;

        this.BottomEditor.FontFamily = "Helvetica Neue";
        this.BottomEditor.FontSize = 18;
        this.BottomEditor.FontAttributes = FontAttributes.Bold;
#endif
        this.provider = provider;
        this.BindingContext = this.vm = this.provider.ResolveWith<ViewModels.TranslationViewModel>();
    }

    /// <inheritdoc/>
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
#if MACCATALYST
        var languageLabel = (UIKit.UILabel)this.LanguageSelectionLabel.Handler!.PlatformView!;
        var tapGesture = new UIKit.UITapGestureRecognizer(HandleTap);
        languageLabel.UserInteractionEnabled = true;
        languageLabel.AddGestureRecognizer(tapGesture);

        // this.FavoriteButton.Text = string.Empty;
        // var gearIcon = UIKit.UIImage.GetSystemImage("gearshape");
        // var button = (UIKit.UIButton)this.SettingsButton.Handler!.PlatformView;
        // button!.SetImage(gearIcon, UIKit.UIControlState.Normal);
#elif WINDOWS
        var languageLabel = (Microsoft.UI.Xaml.Controls.TextBlock)this.LanguageSelectionLabel.Handler!.PlatformView;
        languageLabel.Tapped += LanguageLabel_Tapped;
#endif
    }

#if WINDOWS
    private void LanguageLabel_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        => this.onLanguageSelection.Invoke();
#endif

#if MACCATALYST
    private void HandleTap(UIKit.UITapGestureRecognizer recognizer)
        => this.onLanguageSelection.Invoke();
#endif
}