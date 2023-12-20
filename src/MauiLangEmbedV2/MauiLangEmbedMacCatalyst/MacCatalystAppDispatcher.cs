using Drastic.Services;
using Foundation;

namespace MauiLangEmbedMacCatalyst;

public class MacCatalystAppDispatcher : NSObject, IAppDispatcher
{
    public bool Dispatch(Action action)
    {
        this.InvokeOnMainThread(action);
        return true;
    }
}