using Drastic.Services;

namespace MauiLang;

public class MauiErrorHandler : IErrorHandlerService
{
    public void HandleError(Exception ex)
    {
        Application.Current?.MainPage?.DisplayAlert("Error", ex.Message, "Ok");
    }
}