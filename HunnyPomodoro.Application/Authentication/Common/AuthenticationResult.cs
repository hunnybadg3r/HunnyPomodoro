
using HunnyPomodoro.Domain.User;

namespace HunnyPomodoro.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);