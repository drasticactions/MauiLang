using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiLang.ViewModels;

namespace MauiLangEmbed.Controls;

public partial class FavoritesPage : ContentPage
{
    private FavoritesTranslationViewModel vm;
    
    public FavoritesPage(IServiceProvider provider)
    {
        InitializeComponent();
        this.BindingContext = this.vm = provider.GetRequiredService<FavoritesTranslationViewModel>();
    }
}