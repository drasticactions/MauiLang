using Drastic.Tools;
using MauiLang.ViewModels;
using Microsoft.Maui.Platform;

namespace MauiLang;

public partial class SettingsPage : ContentPage
{
    private IServiceProvider provider;
    private SettingsViewModel vm;
    private OutputResponseLanguagePage outputPage;
    private Action closeModal;
    
    public SettingsPage(IServiceProvider provider, Action closeModal)
    {
        this.InitializeComponent();
        this.closeModal = closeModal;
        this.provider = provider;
        this.outputPage = this.provider.ResolveWith<OutputResponseLanguagePage>();
        this.BindingContext = this.vm = this.provider.ResolveWith<SettingsViewModel>();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

#if WINDOWS
        var languageLabel = (Microsoft.UI.Xaml.Controls.TextBlock)this.LanguageSelectionLabel.Handler!.PlatformView;
        languageLabel.Tapped += LanguageLabel_Tapped;
#endif
    }

#if WINDOWS
    private void LanguageLabel_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        // this.parent.ShowOutputResponseLanguagePage();
    }
#endif
    
    private void Button_OnClicked(object? sender, EventArgs e)
    {
        this.closeModal.Invoke();
    }
}