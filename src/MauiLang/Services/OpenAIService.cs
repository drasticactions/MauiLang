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
    private readonly Settings _settings;
    private IErrorHandlerService _error;
    
    public OpenAIService(IServiceProvider service)
    {
        this._settings = service.GetRequiredService<Settings>();
        this._error = service.GetRequiredService<IErrorHandlerService>();
    }

    public async Task<TranslationResult> GenerateTextAsync(string text)
    {
        if (string.IsNullOrEmpty(_settings.OpenAIToken))
        {
            throw new OpenAIServiceException("OpenAI Token is not set. Please set it in the settings page.");
        }
        
        var api = new OpenAI_API.OpenAIAPI(new APIAuthentication(_settings.OpenAIToken));
        var chat = api.Chat.CreateConversation();
        var language = _settings.TargetLanguage ?? new MauiLangLanguage();
        var responseLanguage = _settings.OutputResponseLanguage ?? new MauiLangLanguage();
        var langOutput = language.LanguageCode;
        var responseLang = responseLanguage.LanguageCode;
        var cultureInfo = CultureInfo.GetCultureInfo(langOutput);
        chat.AppendSystemMessage($"You are a translator that will translate the following dialog into {cultureInfo.EnglishName}. You will translate the text as natural as you can. Match the tone of the given sentence.");
        chat.AppendUserInput(text);
        var result = await chat.GetResponseFromChatbotAsync();
        var json = new TranslationResult() { Translation = result, RespondIn = responseLang, TranslateTo = langOutput, InputText = text}; 
        return json;
    }

    public async Task<TranslationResult> GenerateExplainAsync(TranslationResult result)
    {
        if (string.IsNullOrEmpty(_settings.OpenAIToken))
        {
            throw new OpenAIServiceException("OpenAI Token is not set. Please set it in the settings page.");
        }
        
        var api = new OpenAI_API.OpenAIAPI(new APIAuthentication(_settings.OpenAIToken));
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