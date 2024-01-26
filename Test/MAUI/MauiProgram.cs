using Microsoft.Extensions.Logging;
using MAUI.DataAccess;
using MAUI.ViewsModels;
using MAUI.Views;

namespace MAUI
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

            var dbContext = new VideoGMDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

            builder.Services.AddDbContext<VideoGMDbContext>();

            builder.Services.AddTransient<VideoGamePage>();
            builder.Services.AddTransient<VideoGameVModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            Routing.RegisterRoute(nameof(VideoGamePage), typeof(VideoGamePage));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
