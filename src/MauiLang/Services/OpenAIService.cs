// <copyright file="OpenAIService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Globalization;
using Drastic.Services;
using MauiLang.Models;
using OpenAI_API;

namespace MauiLang.Services;

/// <summary>
/// OpenAI Service.
/// </summary>
public class OpenAIService
{
    private readonly Settings settings;
    private readonly IErrorHandlerService error;

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenAIService"/> class.
    /// </summary>
    /// <param name="service">Service.</param>
    public OpenAIService(IServiceProvider service)
    {
        this.settings = service.GetRequiredService<Settings>();
        this.error = service.GetRequiredService<IErrorHandlerService>();
    }

    /// <summary>
    /// Generates a text translation.
    /// </summary>
    /// <param name="text">Text to translate.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public async Task<TranslationResult> GenerateTextAsync(string text)
    {
        if (string.IsNullOrEmpty(this.settings.OpenAIToken))
        {
            throw new OpenAIServiceException("OpenAI Token is not set. Please set it in the settings page.");
        }

        var api = new OpenAI_API.OpenAIAPI(new APIAuthentication(this.settings.OpenAIToken));
        var chat = api.Chat.CreateConversation();
        var language = this.settings.TargetLanguage ?? new MauiLangLanguage();
        var responseLanguage = this.settings.OutputResponseLanguage ?? new MauiLangLanguage();
        var langOutput = language.LanguageCode;
        var responseLang = responseLanguage.LanguageCode;
        var cultureInfo = CultureInfo.GetCultureInfo(langOutput);
        chat.AppendSystemMessage($"You are a translator that will translate the following dialog into {cultureInfo.EnglishName}. You will translate the text as natural as you can. Match the tone of the given sentence.");
        chat.AppendUserInput(text);
        var result = await chat.GetResponseFromChatbotAsync();
        var json = new TranslationResult() { Translation = result, RespondIn = responseLang, TranslateTo = langOutput, InputText = text};
        return json;
    }

    /// <summary>
    /// Generates an explanation for a translation.
    /// </summary>
    /// <param name="result">The original result.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public async Task<TranslationResult> GenerateExplainAsync(TranslationResult result)
    {
        if (string.IsNullOrEmpty(this.settings.OpenAIToken))
        {
            throw new OpenAIServiceException("OpenAI Token is not set. Please set it in the settings page.");
        }

        var api = new OpenAI_API.OpenAIAPI(new APIAuthentication(this.settings.OpenAIToken));
        var chat = api.Chat.CreateConversation();

        var cultureInfo = CultureInfo.GetCultureInfo(result.TranslateTo);
        var responseL = CultureInfo.GetCultureInfo(result.RespondIn);
        chat.AppendUserInput($"You are a translator. You have translated \"{result.InputText}\" into {cultureInfo.EnglishName}: \"{result.Translation}\". Break down the specifics of how you created your translation in detail, going over the specific grammar choices you used. Write your explanation in {responseL.EnglishName}.");
        var output = await chat.GetResponseFromChatbotAsync();
        result.Explain = output;
        if (string.IsNullOrEmpty(result.Explain))
        {
            result.Explain = Translations.Common.NoExplainLabel;
        }
        return result;
    }
}