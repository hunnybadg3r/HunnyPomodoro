using ErrorOr;
using HunnyPomodoro.Application.Authentication.Common;
using MediatR;

namespace HunnyPomodoro.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;