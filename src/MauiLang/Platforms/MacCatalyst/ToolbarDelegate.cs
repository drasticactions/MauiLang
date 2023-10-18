// <copyright file="ToolbarDelegate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using AppKit;
using Foundation;
using UIKit;

namespace MauiLang;

/// <summary>
/// Toolbar Delegate.
/// </summary>
public class ToolbarDelegate(Window window) : NSToolbarDelegate
{
    private const string Settings = "Settings";
    private Window window = window;

    /// <inheritdoc/>
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
            Settings,
        };
    }

    /// <inheritdoc/>
    public override string[] DefaultItemIdentifiers(NSToolbar toolbar)
    {
        return new string[]
        {
            Settings,
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
        }
    }
}