using HunnyPomodoro.Contracts.Authentication;

namespace HunnyPomodoro.Maui.Services
{
    public interface IAuthService
    {
        Task<bool> IsUserAuthenticated();
        Task<AuthenticationResponse> LoginAsync(LoginRequest request);
        Task<AuthenticationResponse> SignUpAsync(RegisterRequest request);
        Task<AuthenticationResponse?> GetAuthenticatedUserAsync();
        Task<HttpClient> GetAuthenticatedHttpClientAsync();
        void Logout();
    }
}
