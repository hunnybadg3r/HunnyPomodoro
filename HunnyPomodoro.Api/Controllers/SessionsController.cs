using HunnyPomodoro.Application.Sessions.Command.UpdateSession;
using HunnyPomodoro.Application.Sessions.Command.CreateSession;
using HunnyPomodoro.Application.Sessions.Queries;
using HunnyPomodoro.Contracts.Session;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HunnyPomodoro.Api.Controllers;

[Route("users/{userId}/sessions")]
public class SessionsController : ApiControllers
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public SessionsController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession(
        CreateSessionRequest request,
        Guid userId)
    {
        var command = _mapper.Map<CreateSessionCommand>((request, userId));

        var createSessionResult = await _mediator.Send(command);

        return createSessionResult.Match(
            session => Ok(_mapper.Map<SessionResponse>(session)),
            Problem);
    }

    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateSession(
        UpdateSessionRequest request,
        Guid userId)
    {
        var command = _mapper.Map<UpdateSessionCommand>((request, userId));

        var updateSessionResult = await _mediator.Send(command);

        return updateSessionResult.Match(
            session => Ok(_mapper.Map<SessionResponse>(session)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetSessions(
        Guid userId)
    {
        var query = new ListSessionQuery(userId);

        var listSessionResult = await _mediator.Send(query);

        return listSessionResult.Match(
            session => Ok(_mapper.Map<List<SessionResponse>>(session)),
            Problem);
    }
}