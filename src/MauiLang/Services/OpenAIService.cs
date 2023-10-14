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
        chat.AppendUserInput($"You are a translator that will translate dialog into the language given by the user. You will translate the text as natural as you can and write your explanation for how you generated your translation. Your input is in JSON. There are three fields: \"inputText\", which is the text to be translated, \"translateTo\", which accepts a two letter ISO language name, and will be the language you translate \"inputText\" to. And \"respondIn\", which is the language you will write your explanation in. For example, given the following input:\n\n{{\n    \"translateTo\": \"ja\",\n    \"respondIn\": \"en\",\n    \"inputText\": \"Damn! I can't believe he did that!\"\n}}\n\nYour output should be:\n\n{{\n    \"translation\": \"くそっ！彼がそんなことをしたなんて信じられない！\",\n    \"explain\": \"\\\"くそっ！\\\" (Kusott!) is a strong expression of frustration or anger, equivalent to \\\"Damn!\\\" in English.\\r\\n\\\"彼がそんなことをした\\\" (Kare ga sonna koto wo shita) means \\\"he did that,\\\" with \\\"そんなこと\\\" (sonna koto) meaning \\\"that kind of thing\\\" or \\\"such a thing.\\\"\\r\\n\\\"なんて\\\" (nante) is a particle that emphasizes the speaker's disbelief or surprise, similar to \\\"can't believe\\\" in English.\\r\\n\\\"信じられない\\\" (shinjirarenai) is a negative form of the verb \\\"信じる\\\" (shinjiru), meaning \\\"to believe.\\\" So \\\"信じられない\\\" (shinjirarenai) means \\\"can't believe.\\\"\"\n}}\n\nDo you understand?");
        chat.AppendExampleChatbotOutput("Yes, I understand. You would like me to translate the provided text into the language specified in the \"translateTo\" field and provide an explanation of how the translation was generated in the language specified in the \"respondIn\" field. I will follow this format for the given JSON input. Please provide the JSON input, and I will generate the translation and explanation accordingly.");
        chat.AppendUserInput("{\n    \"translateTo\": \"ja\",\n    \"respondIn\": \"en\",\n    \"inputText\": \"Damn! I can't believe he did that!\"\n}");
        chat.AppendExampleChatbotOutput("{\n    \"translation\": \"くそっ！彼がそんなことをしたなんて信じられない！\",\n    \"explain\": \"\\\"くそっ！\\\" (Kusott!) is a strong expression of frustration or anger, equivalent to \\\"Damn!\\\" in English.\\r\\n\\\"彼がそんなことをした\\\" (Kare ga sonna koto wo shita) means \\\"he did that,\\\" with \\\"そんなこと\\\" (sonna koto) meaning \\\"that kind of thing\\\" or \\\"such a thing.\\\"\\r\\n\\\"なんて\\\" (nante) is a particle that emphasizes the speaker's disbelief or surprise, similar to \\\"can't believe\\\" in English.\\r\\n\\\"信じられない\\\" (shinjirarenai) is a negative form of the verb \\\"信じる\\\" (shinjiru), meaning \\\"to believe.\\\" So \\\"信じられない\\\" (shinjirarenai) means \\\"can't believe.\\\"\"\n}");
        chat.AppendUserInput("{\n    \"translateTo\": \"ja\",\n    \"respondIn\": \"ja\",\n    \"inputText\": \"Damn! I can't believe he did that!\"\n}");
        chat.AppendExampleChatbotOutput("{\n\"translation\": \"くそっ！彼がそんなことをしたなんて信じられない！\",\n\"explain\": \"「くそっ！」（Kusott!）は、イライラや怒りを表す強い表現で、英語の「Damn!」に相当します。\\n「彼がそんなことをした」（Kare ga sonna koto wo shita）は、「he did that」と同じ意味で、中でも「そんなこと」（sonna koto）は「そのようなこと」や「そんな出来事」を指します。\\n「なんて」（nante）は、話し手の信じられなさや驚きを強調する助詞で、英語の「can't believe」に似た意味です。\\n「信じられない」（shinjirarenai）は「信じる」という動詞の否定形で、「can't believe」と同じ意味です。\"\n}");
        
        var testObj = new TranslateObj() { translateTo = langOutput, respondIn = responseLang, inputText = text };
        var serialized = System.Text.Json.JsonSerializer.Serialize(testObj, new JsonSerializerOptions() { WriteIndented = true});
        chat.AppendUserInput(serialized);
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