using ErrorOr;
using HunnyPomodoro.Application.Common.Interfaces.Persistence;
using HunnyPomodoro.Domain.Session;
using HunnyPomodoro.Domain.Users.ValueObjects;
using MediatR;

namespace HunnyPomodoro.Application.Sessions.Command.CreateSession;

public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, ErrorOr<Session>>
{
    private readonly ISessionRepository _sessionRepository;

    public CreateSessionCommandHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<ErrorOr<Session>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var session = Session.Create(
            userId: UserId.Create(request.UserId),
            startTime: request.StartTime
        );

        _sessionRepository.Add(session);

        return session;
    }
}
