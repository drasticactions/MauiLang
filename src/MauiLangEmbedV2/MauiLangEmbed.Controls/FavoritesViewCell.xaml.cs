using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiLang.Models;
using MauiLang.Services;

namespace MauiLangEmbed.Controls;

public partial class FavoritesViewCell : VirtualViewCell
{
    public FavoritesViewCell()
    {
        InitializeComponent();
#if MACCATALYST || IOS
        this.OriginalTextLabel.FontFamily = "Helvetica Neue";
        this.OriginalTextLabel.FontSize = 18;
        this.OriginalTextLabel.FontAttributes = FontAttributes.Bold;

        this.TranslatedTextLabel.FontFamily = "Helvetica Neue";
        this.TranslatedTextLabel.FontSize = 18;
        this.TranslatedTextLabel.FontAttributes = FontAttributes.Bold;
#endif
    }

    public TranslationLog TranslationLog
    {
        get => (TranslationLog)this.GetValue(TranslationLogProperty);
        set => this.SetValue(TranslationLogProperty, value);
    }

    public static readonly BindableProperty TranslationLogProperty = BindableProperty.Create(
        nameof(TranslationLog),
        typeof(TranslationLog),
        typeof(FavoritesViewCell),
        default(TranslationLog),
        propertyChanged: OnTranslationLogChanged);

    private static void OnTranslationLogChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is FavoritesViewCell cell)
        {
            cell.BindingContext = cell.TranslationLog = (TranslationLog)newvalue;
        }
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        this.Handler!.MauiContext!.Services.GetRequiredService<DatabaseService>()
            .RemoveFavorite(this.TranslationLog);
    }
}