// <copyright file="OpenAIServiceException.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace MauiLang.Services;

/// <summary>
/// OpenAI Service Exception.
/// </summary>
public class OpenAIServiceException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenAIServiceException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public OpenAIServiceException(string message)
        : base(message)
    {
    }
}