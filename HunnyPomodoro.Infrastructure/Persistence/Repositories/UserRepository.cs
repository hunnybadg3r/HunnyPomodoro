using HunnyPomodoro.Application.Common.Interfaces.Persistence;
using HunnyPomodoro.Domain.User;

namespace HunnyPomodoro.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HunnyPomodoroDbContext _dbContext;

    public UserRepository(HunnyPomodoroDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public User? GetUserByEmail(string email)
    {
        return _dbContext.Users.SingleOrDefault(u => u.Email == email);
    }
}
