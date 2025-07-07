using Microsoft.Extensions.Logging;

namespace WaterSortPuzzle
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<MainPage>();

            //builder.Services.AddSingleton(new MainVM(new MainPage()));
            //builder.Services.AddSingleton<AppPreferences>();
            builder.Services.AddSingleton<MainVM>();
            //builder.Services.AddSingleton<DetailPageVM>();
            //builder.Services.AddSingleton<OptionsVM>();
            //builder.Services.AddSingleton<LoadLevelVM>();

            ////builder.Services.AddTransient<DetailPage>();
            //builder.Services.AddSingleton<Notification>();
            
            //builder.Services.AddSingleton<DetailPage>();
            //builder.Services.AddTransient<OptionsPage>();
            //builder.Services.AddTransient<LoadLevelPage>();

            //builder.Services.AddSingleton<IAlertService, AlertService>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
