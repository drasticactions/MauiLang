// <copyright file="OpenAIServiceException.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace MauiLang.Services;

public class OpenAIServiceException : Exception
{
    public OpenAIServiceException(string message)
        : base(message)
    {
    }
}