
using HunnyPomodoro.Domain.User;

namespace HunnyPomodoro.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}