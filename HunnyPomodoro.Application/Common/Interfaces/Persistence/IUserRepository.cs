using HunnyPomodoro.Domain.User;

namespace HunnyPomodoro.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}