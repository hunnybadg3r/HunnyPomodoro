using HunnyPomodoro.Application.Authentication.Commands.Register;
using HunnyPomodoro.Application.Authentication.Common;
using HunnyPomodoro.Application.Authentication.Queries.Login;
using HunnyPomodoro.Contracts.Authentication;
using HunnyPomodoro.Domain.Users.ValueObjects;
using Mapster;

namespace HunnyPomodoro.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        
        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User)
            .Map(dest => dest.Id, src => UserId.Create(src.User.Id.Value));
    }
}