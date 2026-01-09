using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Handlers.Items;
using CommunityToolkit.Maui;
#if ANDROID
using Android.Views;
using AndroidX.RecyclerView.Widget;
using WaterSortPuzzle.Platforms.Android;
#endif

namespace WaterSortPuzzle
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    //fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    //fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    //fonts.AddFont("fa-brands-400.ttf", "FontAwesomeBrands");
                    fonts.AddFont("fa-regular-400.ttf", "FontAwesomeRegular");
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");

#if WINDOWS
                    // Remap font alias to actual Windows font file URI
                    Microsoft.Maui.Handlers.LabelHandler.Mapper.AppendToMapping("FontFix", (handler, view) =>
                    {
                        if (view is Label label)
                        {
                            if (label.FontFamily == "FontAwesomeSolid")
                                label.FontFamily = "ms-appx:///Resources/Fonts/fa-solid-900.ttf#Font Awesome 6 Pro Solid";
                            else if (label.FontFamily == "FontAwesomeRegular")
                                label.FontFamily = "ms-appx:///Resources/Fonts/fa-regular-400.ttf#Font Awesome 6 Free";
                            else if (label.FontFamily == "FontAwesomeBrands")
                                label.FontFamily = "ms-appx:///Resources/Fonts/fa-brands-400.ttf#Font Awesome 6 Brands";
                        }
                    });
#endif
                });

#if ANDROID
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler<CollectionView, CustomCollectionViewHandler>();
            });

            CollectionViewHandler.Mapper.ModifyMapping(
                nameof(Microsoft.Maui.Controls.CollectionView.ItemsSource),
                (handler, view, action) =>
                {
                    action?.Invoke(handler, view);

                    if (handler.PlatformView is RecyclerView recyclerView)
                    {
                        recyclerView.OverScrollMode = OverScrollMode.Never;
                    }
                });
#endif

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<OptionsPage>();
            builder.Services.AddTransient<LoadLevelPage>();

            //builder.Services.AddSingleton(new MainVM(new MainPage()));
            builder.Services.AddSingleton<MainVM>();
            builder.Services.AddSingleton<OptionsVM>();
            builder.Services.AddSingleton<LoadLevelVM>();
            //builder.Services.AddSingleton<TestovaniDInjectionVM>(); // ##


            //builder.Services.AddTransient<DetailPage>();
            builder.Services.AddSingleton<Notification>();
            builder.Services.AddSingleton<AppPreferences>();
            builder.Services.AddSingleton<GameState>();
            builder.Services.AddSingleton<AutoSolve>();
            builder.Services.AddSingleton<Leveling>();
            builder.Services.AddSingleton<ILevelPreferences, LevelPreferences>();
            //builder.Services.AddTransientPopup<StyledPopup, StyledPopupViewModel>();
            builder.Services.AddTransientPopup<CustomPopup, CustomPopupViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
