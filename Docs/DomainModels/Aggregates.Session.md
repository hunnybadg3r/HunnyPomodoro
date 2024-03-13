# Domain Models

## Session

```csharp
    Session Create(UserId userId, DateTime startTime);
    void UpdateSession(Session session, DateTime startTime, SessionStatus status);
```

```json
    "id": "b0b08a65-2e46-4fd3-b072-3b6d21f0815c",
    "userId": "a1a08a65-8b12-4cd7-a012-1c6e21e0789d",
    "startTime": "2024-03-11T12:30:00Z",
    "endTime": "2024-03-11T12:55:00Z",
    "status": "Done"
```