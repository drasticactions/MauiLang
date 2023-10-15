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
        chat.AppendSystemMessage($"You are a translator that will translate the following dialog into {cultureInfo.EnglishName}. You will translate the text as natural as you can, explain the decisions you made in creating your translation in {responseL.EnglishName}. Match the tone of the given sentence. Your output is JSON. There will be two field, \"translation\" which is where you will write your translation, and \"explain\" where you will write your explanation for how you translated the text");
        var testObj = new TranslateObj() { translateTo = langOutput, respondIn = responseLang, inputText = text };
        var serialized = System.Text.Json.JsonSerializer.Serialize(testObj, new JsonSerializerOptions() { WriteIndented = true});
        chat.AppendUserInput(text);
        var result = await chat.GetResponseFromChatbotAsync();
        var json = JsonSerializer.Deserialize<TranslationResult>(result);
        json.inputText = text;
        json.translateTo = langOutput;
        json.respondIn = responseLang;
        return json;
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