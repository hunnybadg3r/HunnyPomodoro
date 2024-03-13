using HunnyPomodoro.Api.Common.Errors;
using HunnyPomodoro.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HunnyPomodoro.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, HunnyPomodoroProblemDetailsFactory>();
        services.AddMappings();

        return services;
    }
}