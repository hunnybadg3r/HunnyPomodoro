using HunnyPomodoro.Application.Common.Interfaces.Persistence;
using HunnyPomodoro.Domain.Session;
using Microsoft.EntityFrameworkCore;

namespace HunnyPomodoro.Infrastructure.Persistence.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly HunnyPomodoroDbContext _dbContext;

    public SessionRepository(HunnyPomodoroDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Session session)
    {
        _dbContext.Add(session);
        _dbContext.SaveChanges();
    }

    public List<Session> GetSessions(Guid userId)
    {
        return _dbContext.Sessions.Where(d => d.UserId == userId).ToList();
    }

    public Session UpdateSession(
        Guid sessionId,
        Guid userId,
        DateTime endTime,
        SessionStatus status)
    {
        //var session = _dbContext.Sessions.AsEnumerable().SingleOrDefault(d => d.Id.Value == sessionId);
        var session = _dbContext.Sessions.Where(s => s.UserId == userId && s.Status != SessionStatus.Done)
            .AsEnumerable()
            .SingleOrDefault(d => d.Id.Value == sessionId);

        if (session is Session)
        {
            session.EndTime = endTime;
            session.Status = status;
            
            _dbContext.SaveChanges();
        }
        else
        {
            throw new NullReferenceException();
        }
        
        return session;
    }
}