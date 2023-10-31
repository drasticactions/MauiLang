using Drastic.Services;
using Foundation;

namespace MauiLangEmbedMac;

public class MacCatalystAppDispatcher : NSObject, IAppDispatcher
{
    public bool Dispatch(Action action)
    {
        this.InvokeOnMainThread(action);
        return true;
    }
}