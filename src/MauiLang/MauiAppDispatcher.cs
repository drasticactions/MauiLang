using Drastic.Services;

namespace MauiLang;

public class MauiAppDispatcher : IAppDispatcher
{
    public bool Dispatch(Action action)
    {
        return Application.Current?.Dispatcher.Dispatch(action) ?? false;
    }
}