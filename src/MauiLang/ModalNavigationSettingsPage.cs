using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace MauiLang;

public class ModalNavigationSettingsPage : Microsoft.Maui.Controls.NavigationPage
{
    public ModalNavigationSettingsPage(IServiceProvider provider)
        : base(provider.GetRequiredService<SettingsPage>())
    {
        On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.FormSheet);
    }
}

public class ModalTranslationSettingsPage : Microsoft.Maui.Controls.NavigationPage
{
    public ModalTranslationSettingsPage(IServiceProvider provider)
        : base(provider.GetRequiredService<LanguageSelectionPage>())
    {
        On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.FormSheet);
    }
}