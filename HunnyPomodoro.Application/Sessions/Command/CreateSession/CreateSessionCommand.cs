using ErrorOr;
using HunnyPomodoro.Domain.Session;
using MediatR;

namespace HunnyPomodoro.Application.Sessions.Command.CreateSession;

public record CreateSessionCommand(
    Guid UserId,
    DateTime StartTime) : IRequest<ErrorOr<Session>>;