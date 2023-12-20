using Drastic.Tools;
using MauiLang.ViewModels;
using Microsoft.Maui.Platform;

namespace MauiLang;

public partial class SettingsPage : ContentPage
{
    private IServiceProvider provider;
    private SettingsViewModel vm;
    private Action closeModal;
    
    public SettingsPage(IServiceProvider provider, Action closeModal)
    {
        this.InitializeComponent();
        this.closeModal = closeModal;
        this.provider = provider;
        this.BindingContext = this.vm = this.provider.ResolveWith<SettingsViewModel>();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        this.closeModal.Invoke();
    }
}