using HunnyPomodoro.Application.Common.Interfaces.Persistence;
using HunnyPomodoro.Domain.Session;
using HunnyPomodoro.Domain.Session.Events;
using MediatR;

namespace HunnyPomodoro.Application.Sessions.Events;

public class DummyHandler : INotificationHandler<SessionCreated>
{
    private readonly ISessionRepository _sessionRepository;

    public DummyHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public Task Handle(SessionCreated notification, CancellationToken cancellationToken)
    {
        var userId = notification.Session.UserId.Value;

        var sessionsToCancel = _sessionRepository.GetSessions(userId, SessionStatus.InProgress);

        if (sessionsToCancel?.Count > 0)
        {
            sessionsToCancel.ForEach(s => _sessionRepository.UpdateSession(
                s.Id.Value,
                userId,
                DateTime.MinValue,
                SessionStatus.Cancel));
        }

        return Task.CompletedTask;
    }
}