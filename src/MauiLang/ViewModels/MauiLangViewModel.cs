// <copyright file="MauiLangViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.ViewModels;
using MauiLang.Services;

namespace MauiLang.ViewModels;

public class MauiLangViewModel : BaseViewModel
{
    public MauiLangViewModel(IServiceProvider services)
        : base(services)
    {
        this.Database = services.GetRequiredService<DatabaseService>();
        this.OpenAI = services.GetRequiredService<OpenAIService>();
        this.Settings = services.GetRequiredService<Settings>();
    }
    
    public DatabaseService Database { get; }
    
    public OpenAIService OpenAI { get; }
    
    public Settings Settings { get; }
}