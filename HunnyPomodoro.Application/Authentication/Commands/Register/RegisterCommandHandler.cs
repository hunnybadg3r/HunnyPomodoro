using ErrorOr;
using HunnyPomodoro.Application.Authentication.Common;
using HunnyPomodoro.Application.Common.Interfaces.Authentication;
using HunnyPomodoro.Application.Common.Interfaces.Persistence;
using HunnyPomodoro.Domain.Common.Errors;
using HunnyPomodoro.Domain.User;
using MediatR;

namespace HunnyPomodoro.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : 
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        
        var user = User.Create(
            command.Name,
            command.Email,
            command.Password
        );
        
        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}