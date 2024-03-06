namespace HunnyPomodoro.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationResult Register(string name, string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), name, email, "token");
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), "name", email, "token");
    }
}