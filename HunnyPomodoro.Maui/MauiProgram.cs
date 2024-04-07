using HunnyPomodoro.Maui.ViewModel;
using HunnyPomodoro.Maui.View;
using Microsoft.Extensions.Logging;
using HunnyPomodoro.Maui.Services;
using CommunityToolkit.Maui;
using Microcharts.Maui;

namespace HunnyPomodoro.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
			    .UseMauiCommunityToolkit()
                .UseMicrocharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.ttf", "FaSolid");
                });

		    builder.Services.AddCustomApiHttpClient();
            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<ISessionService, SessionService>();

            builder.Services.AddSingleton<TimerPage>();
            builder.Services.AddSingleton<TimerPageViewModel>();

            builder.Services.AddTransient<StatPage>();
		    builder.Services.AddTransient<StatPageViewModel>();
            
            builder.Services.AddTransient<LoginPage>();
		    builder.Services.AddTransient<LoginPageViewModel>();
            
            builder.Services.AddTransient<SignUpPage>();
		    builder.Services.AddTransient<SignUpPageViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
