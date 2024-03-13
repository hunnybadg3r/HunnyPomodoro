using HunnyPomodoro.Domain.Common.Models;

namespace HunnyPomodoro.Domain.Session.ValueObjects;

public sealed class SessionId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private SessionId(Guid value)
    {
        Value = value;
    }

    public static SessionId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static SessionId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private SessionId() { }
}