using CoreGraphics;
using UIKit;

namespace MauiLangEmbed.Controls.Views;

public class MainUIWindow : UIWindow
{
    private MauiContext context;
    
    public MainUIWindow(MauiContext context, CGRect rect)
        : base(rect)
    {
        this.context = context;
        this.RootViewController = new MainViewController(context);
    }
}