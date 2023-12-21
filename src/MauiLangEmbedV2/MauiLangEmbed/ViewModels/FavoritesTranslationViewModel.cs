using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Drastic.ViewModels;
using MauiLang.Models;
using MauiLang.Services;
using Microsoft.Maui.Adapters;

namespace MauiLang.ViewModels;

public class FavoritesTranslationViewModel : MauiLangViewModel
{
    private bool hasNoItems;
    private List<TranslationLog> items;

    public FavoritesTranslationViewModel(IServiceProvider services)
        : base(services)
    {
        this.Database.FavoritesChanged += this.Database_FavoritesChanged;
        this.items = this.Database.GetFavorites();
        this.Items = new VirtualListViewAdapter<TranslationLog>(this.items);
        this.HasNoItems = this.items.Count == 0;
    }

    public VirtualListViewAdapter<TranslationLog> Items { get; }
    
    public bool HasNoItems
    {
        get => this.hasNoItems;
        set => this.SetProperty(ref this.hasNoItems, value);
    }

    private void Database_FavoritesChanged(object? sender, UpdateFavoritesEventArgs e)
    {
        if (e.EventType == ItemEventType.Add)
        {
            this.items.Add(e.Log);
        }
        else if (e.EventType == ItemEventType.Update)
        {
            var index = this.items.FindIndex(x => x.Id == e.Log.Id);
            if (index >= 0)
            {
                this.items[index] = e.Log;
            }
        }
        else if (e.EventType == ItemEventType.Remove)
        {
            var index = this.items.FindIndex(x => x.Id == e.Log.Id);
            if (index >= 0)
            {
                this.items.RemoveAt(index);
            }
        }

        this.Items.InvalidateData();
        this.HasNoItems = this.items.Count == 0;
    }
}