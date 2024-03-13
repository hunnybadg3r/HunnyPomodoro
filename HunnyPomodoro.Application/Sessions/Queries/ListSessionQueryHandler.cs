using ErrorOr;
using HunnyPomodoro.Application.Common.Interfaces.Persistence;
using HunnyPomodoro.Domain.Session;
using MediatR;

namespace HunnyPomodoro.Application.Sessions.Queries;

public class ListSessionQueryHandler : IRequestHandler<ListSessionQuery, ErrorOr<List<Session>>>
{
    private readonly ISessionRepository _sessionRepository;

    public ListSessionQueryHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<ErrorOr<List<Session>>> Handle(ListSessionQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return _sessionRepository.GetSessions(request.UserId);
    }
}
