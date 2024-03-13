using HunnyPomodoro.Application.Common.Interfaces.Services;

namespace HunnyPomodoro.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}