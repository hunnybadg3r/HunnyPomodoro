using HunnyPomodoro.Domain.Common.Models;
using HunnyPomodoro.Domain.Session.ValueObjects;
using HunnyPomodoro.Domain.Users.ValueObjects;

namespace HunnyPomodoro.Domain.User;

public sealed class User : AggregateRoot<UserId, Guid>
{
    private readonly List<SessionId> _sessions = new();

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public IReadOnlyList<SessionId> Sessions => _sessions.AsReadOnly();

    private User(
        UserId id,
        string name,
        string email,
        string password,
        DateTime createdDateTime,
        DateTime updatedDateTime)
        : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static User Create(
        string name,
        string email,
        string password)
    {
        return new(
            UserId.CreateUnique(),
            name,
            email,
            password,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }

#pragma warning disable CS8618
    private User() { }
#pragma warning restore CS8618
}