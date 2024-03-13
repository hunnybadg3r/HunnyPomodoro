using ErrorOr;
using HunnyPomodoro.Application.Authentication.Common;
using MediatR;

namespace HunnyPomodoro.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Name,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;