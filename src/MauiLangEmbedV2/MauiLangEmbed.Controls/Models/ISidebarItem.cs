namespace MauiLangEmbed.Controls.Models;

public interface ISidebarItem
{
    SidebarItemType SidebarItemType { get; }
    
    Action? OnSelected { get; set; }

    bool IsEnabled { get; set; }
}

public enum SidebarItemType
{
    Header,
    Row,
}