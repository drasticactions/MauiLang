// <copyright file="OpenAIService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Globalization;
using Drastic.Services;
using MauiLang.Models;
using Microsoft.Extensions.Logging;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace MauiLang.Services;

/// <summary>
/// OpenAI Service.
/// </summary>
public class OpenAIService
{
    private readonly Settings settings;
    private ILogger? logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenAIService"/> class.
    /// </summary>
    /// <param name="service">Service.</param>
    public OpenAIService(IServiceProvider service)
    {
        this.settings = service.GetRequiredService<Settings>();
        this.logger = service.GetService<ILogger>();
    }

    /// <summary>
    /// Generates a text translation.
    /// </summary>
    /// <param name="text">Text to translate.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public async Task<TranslationLog> GenerateTextAsync(string text)
    {
        if (string.IsNullOrEmpty(this.settings.OpenAIToken))
        {
            throw new OpenAIServiceException("OpenAI Token is not set. Please set it in the settings page.");
        }

        var api = new OpenAI_API.OpenAIAPI(new APIAuthentication(this.settings.OpenAIToken));
        var chat = api.Chat.CreateConversation(new ChatRequest()
        {
            Model = Model.GPT4,
        });
        var language = this.settings.TargetLanguage ?? new MauiLangLanguage();
        var responseLanguage = this.settings.OutputResponseLanguage ?? new MauiLangLanguage();
        var langOutput = language.LanguageCode;
        var responseLang = responseLanguage.LanguageCode;
        var cultureInfo = CultureInfo.GetCultureInfo(langOutput);
        var message =
            $"You are a translator that will translate the following dialog into {cultureInfo.EnglishName}. You will translate the text as natural as you can. Match the tone of the given sentence.";
        chat.AppendSystemMessage(message);
        this.logger?.LogDebug(message);
        chat.AppendUserInput(text);
        this.logger?.LogDebug(text);
        var result = await chat.GetResponseFromChatbotAsync();
        this.logger?.LogDebug(result);
        return new TranslationLog() { Date = DateTime.UtcNow, TranslatedText = result, OutputResponseLanguage = responseLanguage, TargetLanguage = language, OriginalText = text };
    }
}