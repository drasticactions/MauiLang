using System.Globalization;
using System.Text.Json;
using Drastic.Services;
using MauiLang.Models;
using OpenAI_API;

namespace MauiLang.Services;

public class OpenAIService
{
    private IServiceProvider provider;
    private Settings settings;
    private IErrorHandlerService error;
    
    public OpenAIService(IServiceProvider service)
    {
        this.provider = service;
        this.settings = service.GetRequiredService<Settings>();
        this.error = service.GetRequiredService<IErrorHandlerService>();
    }

    public async Task<TranslationResult> GenerateTextAsync(string text)
    {
        if (string.IsNullOrEmpty(settings.OpenAIToken))
        {
            throw new OpenAIServiceException("OpenAI Token is not set. Please set it in the settings page.");
        }
        
        var api = new OpenAI_API.OpenAIAPI(new APIAuthentication(settings.OpenAIToken));
        var chat = api.Chat.CreateConversation();
        var language = settings.TargetLanguage ?? new MauiLangLanguage();
        var responseLanguage = settings.OutputResponseLanguage ?? new MauiLangLanguage();
        var langOutput = language.LanguageCode;
        var responseLang = responseLanguage.LanguageCode;

        var cultureInfo = CultureInfo.GetCultureInfo(langOutput);
        var responseL = CultureInfo.GetCultureInfo(responseLang);
        chat.AppendSystemMessage($"You are a translator that will translate the following dialog into {cultureInfo.EnglishName}. You will translate the text as natural as you can. Match the tone of the given sentence.");
        chat.AppendUserInput(text);
        var result = await chat.GetResponseFromChatbotAsync();
        var json = new TranslationResult() { translation = result, respondIn = responseLang, translateTo = langOutput, inputText = text}; 
        return json;
    }

    public async Task<TranslationResult> GenerateExplainAsync(TranslationResult result)
    {
        if (string.IsNullOrEmpty(settings.OpenAIToken))
        {
            throw new OpenAIServiceException("OpenAI Token is not set. Please set it in the settings page.");
        }
        
        var api = new OpenAI_API.OpenAIAPI(new APIAuthentication(settings.OpenAIToken));
        var chat = api.Chat.CreateConversation();

        var cultureInfo = CultureInfo.GetCultureInfo(result.translateTo);
        var responseL = CultureInfo.GetCultureInfo(result.respondIn);
        chat.AppendUserInput($"You are a translator. You have translated \"{result.inputText}\" into {cultureInfo.EnglishName}: \"{result.translation}\". Break down the specifics of how you created your translation in detail, going over the specific grammar choices you used. Write your explanation in {responseL.EnglishName}.");
        var output = await chat.GetResponseFromChatbotAsync();
        result.explain = output;
        if (string.IsNullOrEmpty(result.explain))
        {
            result.explain = Translations.Common.NoExplainLabel;
        }
        return result;
    }

    private class TranslateObj
    {
        public string translateTo { get; set; }
        
        public string respondIn { get; set; }
        
        public string inputText { get; set; }
    }

    public class OpenAIServiceException : Exception
    {
        public OpenAIServiceException(string message)
            : base(message)
        {
        }
    }
        
}

public class TranslationResult
{
    public string translation { get; set; }
    
    public string explain { get; set; }
    
    public string inputText { get; set; }
    
    public string translateTo { get; set; }
    
    public string respondIn { get; set; }
}