using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Microsoft.Maui.Platform;

public class MauiScrollViewEnable : MauiScrollView
{
    private bool _disableScrolling;

    public MauiScrollViewEnable(Context context)
        : base(context)
    {
    }

    public MauiScrollViewEnable(Context context, IAttributeSet attrs)
        : base(context, attrs)
    {
    }

    public MauiScrollViewEnable(Context context, IAttributeSet attrs, int defStyleAttr)
        : base(context, attrs, defStyleAttr)
    {
    }

    protected MauiScrollViewEnable(IntPtr javaReference, JniHandleOwnership transfer)
        : base(javaReference, transfer)
    {
    }

    public void UpdateIsEnabled(bool isEnabled)
    {
        _disableScrolling = !isEnabled;
    }

    public override bool OnInterceptTouchEvent(MotionEvent? ev)
    {
        if (_disableScrolling)
        {
            return false;
        }

        return base.OnInterceptTouchEvent(ev);
    }

    public override bool OnTouchEvent(MotionEvent? ev)
    {
        if (_disableScrolling)
        {
            return false;
        }

        return base.OnTouchEvent(ev);
    }
}
