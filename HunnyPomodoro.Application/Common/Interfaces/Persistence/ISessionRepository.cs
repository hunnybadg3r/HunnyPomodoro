using HunnyPomodoro.Domain.Session;

namespace HunnyPomodoro.Application.Common.Interfaces.Persistence;

public interface ISessionRepository
{
    void Add(Session session);
    Session UpdateSession(Guid sessionId, Guid userId, DateTime endTime, SessionStatus status);
    List<Session> GetSessions(Guid userId);
}