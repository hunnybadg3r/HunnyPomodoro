namespace HunnyPomodoro.Contracts.Session;

public record UpdateSessionRequest(
    string SessionId,
    DateTime EndTime,
    string Status
);