using HunnyPomodoro.Application.Sessions.Command.CreateSession;
using HunnyPomodoro.Application.Sessions.Command.UpdateSession;
using HunnyPomodoro.Contracts.Session;
using HunnyPomodoro.Domain.Session;
using Mapster;

namespace HunnyPomodoro.Api.Common.Mapping;

public class SessionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateSessionRequest Request, Guid UserId), CreateSessionCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<(UpdateSessionRequest Request, Guid UserId), UpdateSessionCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<Session, SessionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.UserId, src => src.UserId.Value);
    }
}
