using LoginBlazorApp.Services;
using Microsoft.Extensions.Logging;

namespace LoginBlazorApp
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
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IAppService, AppService>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://your-api-base-url/") });

#if ANDROID
            //builder.Services.AddSingleton<ISpeechToText, LoginBlazorApp.Platforms.Android.SpeechToTextImplementation>();
#elif WINDOWS
            builder.Services.AddSingleton<ISpeechToText, LoginBlazorApp.Platforms.Windows.SpeechToTextImplementation>();
#endif
           // builder.Services.AddSingleton<SpeechToTextService>();

            return builder.Build();
        }
    }
}
