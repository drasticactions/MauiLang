using Drastic.Tools;
using MauiLang.ViewModels;

namespace MauiLang;

public partial class SettingsPage : ContentPage
{
    private IServiceProvider provider;
    private SettingsViewModel vm;
    private OutputResponseLanguagePage outputPage;
    private INativeNavigation? parent;

    public SettingsPage(IServiceProvider provider, INativeNavigation? parent = default)
    {
        InitializeComponent();
        this.parent = parent;
        this.provider = provider;
        this.outputPage = this.provider.GetRequiredService<OutputResponseLanguagePage>();
        this.BindingContext = this.vm = this.provider.ResolveWith<SettingsViewModel>(parent);
    }

    private void OutputLanguageTapped(object sender, TappedEventArgs e)
    {
        this.Navigation.PushAsync(this.outputPage);
    }
}