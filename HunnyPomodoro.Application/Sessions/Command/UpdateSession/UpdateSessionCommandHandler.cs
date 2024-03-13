using ErrorOr;
using HunnyPomodoro.Application.Common.Interfaces.Persistence;
using HunnyPomodoro.Domain.Session;
using MediatR;

namespace HunnyPomodoro.Application.Sessions.Command.UpdateSession;

public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, ErrorOr<Session>>
{
    private readonly ISessionRepository _sessionRepository;

    public UpdateSessionCommandHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<ErrorOr<Session>> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return _sessionRepository.UpdateSession(request.SessionId, request.UserId, request.EndTime, request.Status);
    }
}
