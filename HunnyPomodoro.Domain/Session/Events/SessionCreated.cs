using HunnyPomodoro.Domain.Common.Models;

namespace HunnyPomodoro.Domain.Session.Events;

public record SessionCreated(Session Session) : IDomainEvent;