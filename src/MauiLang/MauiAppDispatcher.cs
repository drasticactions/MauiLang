// <copyright file="MauiAppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;

namespace MauiLang;

public class MauiAppDispatcher : IAppDispatcher
{
    public bool Dispatch(Action action)
    {
        return Application.Current?.Dispatcher.Dispatch(action) ?? false;
    }
}