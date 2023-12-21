using Drastic.Tools;
using MauiLang;
using MauiLang.Translations;
using MauiLangEmbed.Controls.Models;
using Microsoft.Maui.Platform;
using UIKit;

namespace MauiLangEmbed.Controls.Views;

public class MainViewController : UISplitViewController
{
    private SidebarUIViewController sidebar;
    private UIViewController mainViewController;
    private UIViewController languageSelectionViewController;
    private UIViewController settingsViewController;
    private UIViewController favoritesViewController;
    private MauiContext context;
    private SidebarUIViewControllerOptions options;

    public MainViewController(MauiContext context)
        : base(UISplitViewControllerStyle.DoubleColumn)
    {
        this.Title = MauiLang.Translations.Common.AppName;
        this.context = context;

        var mainView = context.Services.ResolveWith<MainPage>(this.OpenLanguageModal);
        this.mainViewController = mainView.ToUIViewController(this.context);

        var languageView = context.Services.ResolveWith<LanguageSelectionPage>(this.CloseModal);
        this.languageSelectionViewController = languageView.ToUIViewController(this.context);

        var settingsView = context.Services.ResolveWith<SettingsPage>(this.CloseModal);
        this.settingsViewController = settingsView.ToUIViewController(this.context);

        var favoritesView = context.Services.ResolveWith<FavoritesPage>();
        this.favoritesViewController = favoritesView.ToUIViewController(this.context);

        this.options = new SidebarUIViewControllerOptions();
        this.options.HeaderItems.Add(new SidebarHeaderItem(
            MauiLang.Translations.Common.TranslateLabel,
            new List<SidebarItem>
            {
                new(
                    MauiLang.Translations.Common.TranslateLabel,
                    UIImage.GetSystemImage("square.and.pencil.circle"))
                {
                    OnSelected = () =>
                    {
                        this.SetViewController(null, UISplitViewControllerColumn.Secondary);
                        this.SetViewController(this.mainViewController, UISplitViewControllerColumn.Secondary);
                    },
                },
                new(
                    MauiLang.Translations.Common.FavoriteLabel,
                    UIImage.GetSystemImage("star.circle"))
                {
                    OnSelected = () =>
                    {
                        this.SetViewController(null, UISplitViewControllerColumn.Secondary);
                        this.SetViewController(this.favoritesViewController, UISplitViewControllerColumn.Secondary);
                    },
                },
            }));

        this.options.MenuItemsBelowHeader.Add(new(
            Common.SettingsLabel,
            UIImage.GetSystemImage("gearshape"))
        {
            OnSelected = () =>
            {
                this.SetViewController(null, UISplitViewControllerColumn.Secondary);
                this.SetViewController(this.settingsViewController, UISplitViewControllerColumn.Secondary);
            },
        });

        this.sidebar = new SidebarUIViewController(this.options);
        this.sidebar.OnItemSelected += this.SidebarOnOnItemSelected;
        this.SetViewController(this.sidebar, UISplitViewControllerColumn.Primary);
        this.PreferredPrimaryColumnWidth = 200;
#if !TVOS
        this.PrimaryBackgroundStyle = UISplitViewControllerBackgroundStyle.Sidebar;
#endif
    }

    private async void OpenLanguageModal()
    {
        await this.PresentViewControllerAsync(this.languageSelectionViewController, true);
    }

    private async void CloseModal()
    {
        await this.DismissViewControllerAsync(true);
    }

    private void SidebarOnOnItemSelected(object? sender, SidebarSelectionEventArgs e)
    {
        e.Item.OnSelected?.Invoke();
    }
}