using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Maui.Platform;

namespace WaterSortPuzzle
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestedOrientation = ScreenOrientation.Portrait;

            UpdateNavigationBar();
        }
#pragma warning disable CA1422, CA1416
        private void UpdateNavigationBar()
        {
            var newColor = Android.Graphics.Color.Black;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu) // API 33+ is Android 13
            {
                // For Android 14 / API 35+, you can use WindowInsetsController and system bar appearance
                var insetsController = Window?.InsetsController;

                if (insetsController != null)
                {
                    // Set navigation bar background
                    Window?.SetNavigationBarColor(newColor); // still works, but obsolete

                    insetsController.SetSystemBarsAppearance(
                        (int)WindowInsetsControllerAppearance.LightNavigationBars,
                        (int)WindowInsetsControllerAppearance.LightNavigationBars);
                }
            }
            else if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) // API 21+
            {
                // Pre-35 standard call
                Window?.SetNavigationBarColor(newColor);

                if (Build.VERSION.SdkInt >= BuildVersionCodes.R) // API 30+
                {
                    Window?.InsetsController?.SetSystemBarsAppearance(
                        (int)WindowInsetsControllerAppearance.LightNavigationBars,
                        (int)WindowInsetsControllerAppearance.LightNavigationBars);
                }
            }
        }
#pragma warning restore CA1416
    }
}
