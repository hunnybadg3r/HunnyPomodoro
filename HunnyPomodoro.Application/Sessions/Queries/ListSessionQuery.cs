using ErrorOr;
using HunnyPomodoro.Domain.Session;
using MediatR;

namespace HunnyPomodoro.Application.Sessions.Queries;

public record ListSessionQuery(
    Guid UserId) : IRequest<ErrorOr<List<Session>>>;