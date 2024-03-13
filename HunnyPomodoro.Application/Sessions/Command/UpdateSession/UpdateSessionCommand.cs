using ErrorOr;
using HunnyPomodoro.Domain.Session;
using MediatR;

namespace HunnyPomodoro.Application.Sessions.Command.UpdateSession;

public record UpdateSessionCommand(
    Guid SessionId,
    Guid UserId,
    DateTime EndTime,
    SessionStatus Status) : IRequest<ErrorOr<Session>>;