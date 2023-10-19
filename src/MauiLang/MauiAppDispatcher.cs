// <copyright file="MauiAppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;

namespace MauiLang;

/// <summary>
/// Maui App Dispatcher.
/// </summary>
public class MauiAppDispatcher : IAppDispatcher
{
    /// <inheritdoc/>
    public bool Dispatch(Action action)
    {
        return Application.Current?.Dispatcher.Dispatch(action) ?? false;
    }
}