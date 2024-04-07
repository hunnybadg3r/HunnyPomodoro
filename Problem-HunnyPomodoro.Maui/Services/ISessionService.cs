using HunnyPomodoro.Contracts.Session;

namespace HunnyPomodoro.Maui.Services
{
    public interface ISessionService
    {
        Task<SessionResponse> CreateSessionAsync(CreateSessionRequest request);
        Task<IEnumerable<SessionResponse>> GetSessionsAsync();
        Task<SessionResponse> UpdateSessionAsync(UpdateSessionRequest request);

    }
}
