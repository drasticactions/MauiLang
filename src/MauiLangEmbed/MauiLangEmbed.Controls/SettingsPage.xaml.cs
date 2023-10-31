using Drastic.Tools;
using MauiLang.ViewModels;
using Microsoft.Maui.Platform;

namespace MauiLang;

public partial class SettingsPage : ContentPage
{
    private IServiceProvider provider;
    private SettingsViewModel vm;
    private OutputResponseLanguagePage outputPage;
    private INativeNavigation parent;
    private IModalNavigation modal;

    public SettingsPage(IServiceProvider provider, IModalNavigation modal, INativeNavigation parent)
    {
        InitializeComponent();
        this.modal = modal;
        this.parent = parent;
        this.provider = provider;
        this.outputPage = this.provider.ResolveWith<OutputResponseLanguagePage>(parent);
        this.BindingContext = this.vm = this.provider.ResolveWith<SettingsViewModel>(modal, parent);
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
        this.parent.ShowOutputResponseLanguagePage();
    }
#endif
}