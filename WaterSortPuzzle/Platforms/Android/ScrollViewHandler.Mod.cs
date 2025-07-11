using Microsoft.Maui.Handlers;
using Android.Views; // Needed for OverScrollMode
using Android.Widget;
using AView = Android.Views.View;
using AScrollView = Android.Widget.ScrollView;

namespace YourApp.Platforms.Android;

//public partial class CustomScrollViewHandler : ScrollViewHandler
//{
//    protected override void ConnectHandler(AScrollView nativeView)
//    {
//        base.ConnectHandler(nativeView);
//        nativeView.OverScrollMode = OverScrollMode.Never;
//    }
//}
//public class CustomScrollViewHandler : ScrollViewHandler
//{
//    public static IPropertyMapper<ScrollView, ScrollViewHandler> CustomMapper =
//        new PropertyMapper<ScrollView, ScrollViewHandler>(ScrollViewHandler.Mapper)
//        {
//            [nameof(ScrollView.Content)] = MapContent
//        };

//    public CustomScrollViewHandler() : base(CustomMapper)
//    {
//    }

//    public static void MapContent(ScrollViewHandler handler, ScrollView scrollView)
//    {
//        // Call the base handler
//        //ScrollViewHandler.Mapper[nameof(ScrollView.Content)]?.Invoke(handler, scrollView);
//        var baseMapper = ScrollViewHandler.Mapper;
//        if (baseMapper.TryGetValue(nameof(ScrollView.Content), out var baseMap))
//        {
//            baseMap?.Invoke(handler, scrollView);
//        }

//        if (handler.PlatformView is AScrollView nativeScroll)
//        {
//            nativeScroll.OverScrollMode = OverScrollMode.Never;
//        }
//    }
//}
//public class CustomScrollViewHandler : ScrollViewHandler
//{
//    public static IPropertyMapper<ScrollView, ScrollViewHandler> CustomMapper =
//        new PropertyMapper<ScrollView, ScrollViewHandler>(ScrollViewHandler.Mapper)
//        {
//            [nameof(ScrollView.Content)] = MapContent
//        };

//    public CustomScrollViewHandler() : base(CustomMapper)
//    {
//    }

//    public static void MapContent(ScrollViewHandler handler, ScrollView scrollView)
//    {
//        // First, call the base method directly without using index access
//        ScrollViewHandler.Mapper.UpdateProperties(handler, scrollView);

//        // Then, set the overscroll mode to disable bounce
//        if (handler.PlatformView is AScrollView nativeScroll)
//        {
//            nativeScroll.OverScrollMode = OverScrollMode.Never;
//        }
//    }
//}

//public class CustomScrollViewHandler : ScrollViewHandler
//{
//    public static IPropertyMapper<Microsoft.Maui.Controls.ScrollView, ScrollViewHandler> CustomMapper =
//        new PropertyMapper<Microsoft.Maui.Controls.ScrollView, ScrollViewHandler>(ScrollViewHandler.Mapper)
//        {
//            [nameof(Microsoft.Maui.Controls.ScrollView.Content)] = MapContent
//        };

//    public CustomScrollViewHandler() : base(CustomMapper)
//    {
//    }

//    public static void MapContent(ScrollViewHandler handler, Microsoft.Maui.Controls.ScrollView scrollView)
//    {
//        // Call base mapping logic
//        ScrollViewHandler.Mapper.UpdateProperties(handler, scrollView);

//        // Set OverScrollMode = Never if native view is Android.Widget.ScrollView
//        var nativeScroll = handler.PlatformView as AScrollView;
//        if (nativeScroll != null)
//        {
//            nativeScroll.OverScrollMode = OverScrollMode.Never;
//        }
//    }
//}

public class CustomScrollViewHandler : ScrollViewHandler
{
    public static IPropertyMapper<Microsoft.Maui.Controls.ScrollView, ScrollViewHandler> CustomMapper =
        new PropertyMapper<Microsoft.Maui.Controls.ScrollView, ScrollViewHandler>(ScrollViewHandler.Mapper)
        {
            [nameof(Microsoft.Maui.Controls.ScrollView.Content)] = MapContent
        };

    public CustomScrollViewHandler() : base(CustomMapper)
    {
    }

    public static void MapContent(ScrollViewHandler handler, Microsoft.Maui.Controls.ScrollView scrollView)
    {
        ScrollViewHandler.Mapper.UpdateProperties(handler, scrollView);

        // Traverse the view hierarchy to find the native Android ScrollView
        if (handler.PlatformView is ViewGroup viewGroup)
        {
            for (int i = 0; i < viewGroup.ChildCount; i++)
            {
                if (viewGroup.GetChildAt(i) is AScrollView nativeScroll)
                {
                    nativeScroll.OverScrollMode = OverScrollMode.Never;
                }
            }
        }
    }
}