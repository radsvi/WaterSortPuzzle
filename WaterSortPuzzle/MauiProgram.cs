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

            //builder.Services.AddSingleton<MainVM>();
            //builder.Services.AddSingleton(new MainVM(new MainPage()));
            //builder.Services.AddSingleton<AppSettings>();
            builder.Services.AddSingleton<DetailPageVM>();

            builder.Services.AddSingleton<MainPage>();
            //builder.Services.AddTransient<DetailPage>();
            builder.Services.AddSingleton<DetailPage>();
            builder.Services.AddSingleton<Notification>();


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
