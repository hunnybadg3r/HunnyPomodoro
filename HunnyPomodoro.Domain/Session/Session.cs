using HunnyPomodoro.Domain.Common.Models;
using HunnyPomodoro.Domain.Session.Events;
using HunnyPomodoro.Domain.Session.ValueObjects;
using HunnyPomodoro.Domain.Users.ValueObjects;

namespace HunnyPomodoro.Domain.Session;

public sealed class Session : AggregateRoot<SessionId, Guid>
{
    public UserId UserId { get; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; set; }
    public SessionStatus Status { get; set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; set; }

    private Session(
        SessionId id,
        UserId userId,
        DateTime startTime,
        DateTime endTime,
        SessionStatus status,
        DateTime createdDateTime,
        DateTime updatedDateTime)
        : base(id)
    {
        UserId = userId;
        StartTime = startTime;
        EndTime = endTime;
        Status = status;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Session Create(
        UserId userId,
        DateTime startTime)
    {
        var utcNow = DateTime.UtcNow;
        var session = new Session(
            SessionId.CreateUnique(),
            userId,
            startTime,
            DateTime.MinValue,
            SessionStatus.InProgress,
            utcNow,
            utcNow
        );

        session.AddDomainEvent(new SessionCreated(session));

        return session;
    }

    public void Update(Session session, DateTime endTime, SessionStatus status)
    {
        session.EndTime = endTime;
        session.Status = status;
    }

#pragma warning disable CS8618
    private Session() { }
#pragma warning restore CS8618
}