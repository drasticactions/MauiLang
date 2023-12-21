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
    public DatabaseService(string? path = default)
    {
        path = path ?? Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        Directory.CreateDirectory(path);
        this._db = new LiteDatabase(Path.Combine(path, "mauilangembed.db"));
    }

    /// <summary>
    /// Gets the Settings Collection.
    /// </summary>
    private ILiteCollection<Settings> Settings => this._db.GetCollection<Settings>();

    private ILiteCollection<TranslationLog> Favorites => this._db.GetCollection<TranslationLog>();

    public event EventHandler<UpdateSettingsEventArgs>? SettingsChanged;
    
    public event EventHandler<UpdateFavoritesEventArgs>? FavoritesChanged;
    
    public void AddFavorite(TranslationLog log)
    {
        lock (this._db)
        {
            if (this.Favorites.Exists(x => x.Id == log.Id))
            {
                this.Favorites.Update(log);
                this.FavoritesChanged?.Invoke(this, new UpdateFavoritesEventArgs(log, ItemEventType.Update));
            }
            else
            {
                this.Favorites.Insert(log);
                this.FavoritesChanged?.Invoke(this, new UpdateFavoritesEventArgs(log, ItemEventType.Add));
            }
        }
    }

    public void RemoveFavorite(TranslationLog log)
    {
        lock (this._db)
        {
            this.Favorites.Delete(log.Id);
            this.FavoritesChanged?.Invoke(this, new UpdateFavoritesEventArgs(log, ItemEventType.Remove));
        }
    }

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

            return settings;
        }
    }
    
    public List<TranslationLog> GetFavorites()
    {
        lock (this._db)
        {
            return this.Favorites.FindAll().ToList();
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

        this.SettingsChanged?.Invoke(this, new UpdateSettingsEventArgs(settings));
    }
}

public class UpdateSettingsEventArgs : EventArgs
{
    public UpdateSettingsEventArgs(Settings settings)
    {
        this.Settings = settings;
        this.EventType = ItemEventType.Update;
    }

    public Settings Settings { get; }
    
    public ItemEventType EventType { get; }
}

public class UpdateFavoritesEventArgs : EventArgs
{
    public UpdateFavoritesEventArgs(TranslationLog log, ItemEventType type)
    {
        this.Log = log;
        this.EventType = type;
    }

    public TranslationLog Log { get; }
    
    public ItemEventType EventType { get; }
}

public enum ItemEventType
{
    Add,
    Remove,
    Update,
}