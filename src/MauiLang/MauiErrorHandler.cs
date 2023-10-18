// <copyright file="MauiErrorHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;

namespace MauiLang;

/// <summary>
/// Maui Error Handler.
/// </summary>
public class MauiErrorHandler : IErrorHandlerService
{
    /// <inheritdoc/>
    public void HandleError(Exception ex)
    {
        Application.Current?.MainPage?.DisplayAlert("Error", ex.Message, "Ok");
    }
}