using HunnyPomodoro.Domain.Common.Models;
using HunnyPomodoro.Domain.Session.ValueObjects;
using HunnyPomodoro.Domain.Users.ValueObjects;

namespace HunnyPomodoro.Domain.Session;

public sealed class Session : AggregateRoot<SessionId, Guid>
{
    public UserId UserId { get; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; set; }
    public SessionStatus Status { get; set; }


    private Session(
        SessionId sessionId,
        UserId userId)
        : base(sessionId)
    {
        UserId = userId;
    }

    public static Session Create(
        UserId userId,
        DateTime startTime)
    {
        var session = new Session(
            SessionId.CreateUnique(),
            userId)
            {
                StartTime = startTime,
                Status = SessionStatus.InProgress
            };

        return session;
    }

    public void Update(Session session, DateTime endTime, SessionStatus status)
    {
        session.EndTime = EndTime;
        session.Status = status;
    }

#pragma warning disable CS8618
    private Session() { }
#pragma warning restore CS8618
}