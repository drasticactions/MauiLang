using AppKit;
using Foundation;
using UIKit;

namespace MauiLang;

public class ToolbarDelegate : NSToolbarDelegate
{
    private const string Settings = "Settings";
    private Window window;
    public ToolbarDelegate(Window window)
    {
        this.window = window;
    }
    public override NSToolbarItem WillInsertItem(NSToolbar toolbar, string itemIdentifier, bool willBeInserted)
    {
        NSToolbarItem toolbarItem = new NSToolbarItem(itemIdentifier);
        if (itemIdentifier == Settings)
        {
            toolbarItem.UIImage = UIImage.GetSystemImage("gearshape");
            toolbarItem.Action = new ObjCRuntime.Selector("buttonClickAction:");
            toolbarItem.Target = this;
            toolbarItem.Label = "Settings";
            toolbarItem.Enabled = true;
        }

        return toolbarItem;
    }

    /// <inheritdoc/>
    public override string[] AllowedItemIdentifiers(NSToolbar toolbar)
    {
        return new string[]
        {
            Settings
        };
    }


    public override string[] DefaultItemIdentifiers(NSToolbar toolbar)
    {
        return new string[]
        {
            Settings
        };
    }

    [Export("buttonClickAction:")]
    public async void ButtonClickAction(NSObject sender)
    {
        var settings = this.window.Handler?.MauiContext?.Services?.GetRequiredService<ModalNavigationSettingsPage>();
        if (settings != null && this.window?.Page?.Navigation != null)
        {
            await this.window.Page.Navigation.PushModalAsync(settings);
            
            // Control the size of the resulting modal.
            // MAUI wraps modals in a ControlModalWrapper class,
            // setting the size of its PrefferedContentSize property will control the size of the modal.
            // but you can't do it until it's already created.

            //var test = this.GetModalWrapper(settings);
            //test.PreferredContentSize = new CoreGraphics.CGSize(300, 400);
        }
    }

    internal UIViewController GetModalWrapper(Page modalPage)
	{
			var pageVC = (modalPage.Handler as IPlatformViewHandler).ViewController;
			return (UIViewController)pageVC.ParentViewController;
    }
}