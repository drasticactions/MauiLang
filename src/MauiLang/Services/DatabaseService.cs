using System.Globalization;
using LiteDB;
using MauiLang.Models;

namespace MauiLang.Services;

public class DatabaseService
{
    private readonly LiteDatabase _db;
    
    public DatabaseService()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        _db = new LiteDatabase(Path.Combine(path, "mauilang.db"));
    }
    
    public ILiteCollection<Settings> Settings => _db.GetCollection<Settings>();
    
    public Settings GetSettings()
    {
        lock (_db)
        {
            var settings = Settings.FindAll().FirstOrDefault();
            if (settings == null)
            {
                settings = new Settings();
                Settings.Insert(settings);
            }

            if (settings.TargetLanguage is not null)
            {
                settings.TargetLanguage.CultureInfo = CultureInfo.GetCultureInfo(settings.TargetLanguage.LanguageCode);
            }
            
            if (settings.OutputResponseLanguage is not null)
            {
                settings.OutputResponseLanguage.CultureInfo = CultureInfo.GetCultureInfo(settings.OutputResponseLanguage.LanguageCode);
            }
            return settings;
        }
    }
    
    public void SetSettings(Settings settings)
    {
        lock (_db)
        {
            Settings.Upsert(settings);
        }
    }
}

public class Settings
{
    public int Id { get; set; }
    
    public string OpenAIToken { get; set; }
    
    public MauiLangLanguage? TargetLanguage { get; set; }
    
    public MauiLangLanguage? OutputResponseLanguage { get; set; }
}