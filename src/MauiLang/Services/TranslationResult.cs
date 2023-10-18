// <copyright file="TranslationResult.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace MauiLang.Services;

public class TranslationResult
{
    public string Translation { get; set; }
    
    public string Explain { get; set; }
    
    public string InputText { get; set; }
    
    public string TranslateTo { get; set; }
    
    public string RespondIn { get; set; }
}