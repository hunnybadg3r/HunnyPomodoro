
namespace HunnyPomodoro.Contracts.Session;

public record SessionResponse(
    string Id,
    string UserId,
    DateTime StartTime,
    DateTime EndTime,
    string Status
);
