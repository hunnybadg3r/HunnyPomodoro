using HunnyPomodoro.Maui;

public static class MauiProgramExtensions
{
    public static IServiceCollection AddCustomApiHttpClient(this IServiceCollection services)
    {
        services.AddSingleton<IPlatformHttpMessageHandler>(_ =>
        {
#if ANDROID
            return new AndroidHttpMessageHandler();
#endif
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS0162 // Unreachable code detected
            return null;
#pragma warning restore CS0162 // Unreachable code detected
#pragma warning restore CS8603 // Possible null reference return.
        });

        services.AddHttpClient(AppConstants.HttpClientName, httpClient =>
        {
            var baseAddress =
                    DeviceInfo.Platform == DevicePlatform.Android
                        ? "https://10.0.2.2:7217"
                        : "https://localhost:7217";

            httpClient.BaseAddress = new Uri(baseAddress);
        }).ConfigurePrimaryHttpMessageHandler(builder =>
            {
                var platformHttpMessageHandler = builder.GetRequiredService<IPlatformHttpMessageHandler>();
                return platformHttpMessageHandler.GetHttpMessageHandler();
            });

        //TODO check new ConfigurePrimaryHttpMessageHandler and delete this old one
        //.ConfigureHttpMessageHandlerBuilder(builder =>
        //{
        //    var platfromHttpMessageHandler = builder.Services.GetRequiredService<IPlatformHttpMessageHandler>();
        //    builder.PrimaryHandler = platfromHttpMessageHandler.GetHttpMessageHandler();
        //});

        return services;
    }
}
