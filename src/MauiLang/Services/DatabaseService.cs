// <copyright file="DatabaseService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Globalization;
using LiteDB;
using MauiLang.Models;

namespace MauiLang.Services;

/// <summary>
/// Database Service.
/// </summary>
public class DatabaseService
{
    private readonly LiteDatabase _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseService"/> class.
    /// </summary>
    public DatabaseService()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        this._db = new LiteDatabase(Path.Combine(path, "mauilang.db"));
    }

    /// <summary>
    /// Gets the Settings Collection.
    /// </summary>
    public ILiteCollection<Settings> Settings => this._db.GetCollection<Settings>();

    /// <summary>
    /// Get Settings.
    /// </summary>
    /// <returns>Settings.</returns>
    public Settings GetSettings()
    {
        lock (this._db)
        {
            var settings = this.Settings.FindAll().FirstOrDefault();
            if (settings == null)
            {
                settings = new Settings();
                this.Settings.Insert(settings);
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

    /// <summary>
    /// Set Settings.
    /// </summary>
    /// <param name="settings">Settings.</param>
    public void SetSettings(Settings settings)
    {
        lock (this._db)
        {
            this.Settings.Upsert(settings);
        }
    }
}