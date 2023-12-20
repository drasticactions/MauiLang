namespace MauiLang.Models;

public class TranslationLog
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string OriginalText { get; set; }

    public string TranslatedText { get; set; }

    public MauiLangLanguage TargetLanguage { get; set; }

    public MauiLangLanguage OutputResponseLanguage { get; set; }
}