using _Microsoft.Android.Resource.Designer;
using global::Android.Content;
using global::Android.Runtime;
using global::Android.Util;
using global::Android.Views;

using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

public class ScrollViewEnableHandler : ScrollViewHandler
{
    public ScrollViewEnableHandler()
    {
        Mapper.AppendToMapping(
            nameof(IScrollView.IsEnabled),
            (handler, view) =>
                ((MauiScrollViewEnable)handler.PlatformView)
                    .UpdateIsEnabled(view.IsEnabled));
    }

    protected override MauiScrollView CreatePlatformView()
    {
        var scrollView = new MauiScrollViewEnable(
            new ContextThemeWrapper(MauiContext!.Context, ResourceConstant.Style.scrollViewTheme),
            null!,
            ResourceConstant.Attribute.scrollViewStyle)
        {
            ClipToOutline = true,
            FillViewport = true,
        };

        return scrollView;
    }
}
