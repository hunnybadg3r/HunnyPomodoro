using System.Text;
using HunnyPomodoro.Application.Common.Interfaces.Authentication;
using HunnyPomodoro.Application.Common.Interfaces.Persistence;
using HunnyPomodoro.Application.Common.Interfaces.Services;
using HunnyPomodoro.Infrastructure.Authentication;
using HunnyPomodoro.Infrastructure.Persistence;
using HunnyPomodoro.Infrastructure.Persistence.Interceptors;
using HunnyPomodoro.Infrastructure.Persistence.Repositories;
using HunnyPomodoro.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HunnyPomodoro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration)
                .AddPersistance();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

 public static IServiceCollection AddPersistance(
        this IServiceCollection services)
    {
        services.AddDbContext<HunnyPomodoroDbContext>(options =>
            options.UseSqlServer("Server=localhost;Database=HunnyPomodoro;User Id=sa;Password=eyejoa123!;TrustServerCertificate=True"));

        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();

        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, jwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, 
                ValidateAudience = true, 
                ValidateLifetime = true, 
                ValidateIssuerSigningKey = true, 
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }
}