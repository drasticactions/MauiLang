using Drastic.Services;
using MauiLang.Services;
using MauiLang.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiLang;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
#if MACCATALYST
		Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping("ButtonChange", (handler, view) =>
        {
            handler.PlatformView.PreferredBehavioralStyle = UIKit.UIBehavioralStyle.Pad;
            handler.PlatformView.Layer.CornerRadius = 5;
            handler.PlatformView.ClipsToBounds = true;
        });
#endif
		
		#if IOS || MACCATALYST
		Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("EntryChange", (handler, view) =>
		{
			handler.PlatformView.InputAccessoryView = null;
			var centeredParagraphStyle = new UIKit.NSMutableParagraphStyle();
			centeredParagraphStyle.Alignment = UIKit.UITextAlignment.Center;
			var attributedPlaceholder = new Foundation.NSAttributedString(
				view.Placeholder,
				new UIKit.UIStringAttributes
				{
					ParagraphStyle = centeredParagraphStyle
				}
			);
			
			handler.PlatformView.AttributedPlaceholderText = attributedPlaceholder;
		});
		#endif

		var databaseService = new DatabaseService();
		var settings = databaseService.GetSettings();
		var builder = MauiApp.CreateBuilder();
		builder.Services
			.AddSingleton<IAppDispatcher, MauiAppDispatcher>()
			.AddSingleton<IErrorHandlerService, MauiErrorHandler>()
			.AddSingleton<OpenAIService>()
			.AddSingleton<DatabaseService>(databaseService)
			.AddSingleton<Settings>(settings)
			.AddSingleton<SettingsViewModel>()
			.AddSingleton<OutputResponseLanguageViewModel>()
			.AddSingleton<TargetLanguageViewModel>();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
