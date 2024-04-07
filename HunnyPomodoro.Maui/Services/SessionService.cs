using System.Net.Http.Json;
using System.Text.Json;
using HunnyPomodoro.Contracts.Session;

namespace HunnyPomodoro.Maui.Services
{
    public class SessionService : ISessionService
    {
        private readonly IAuthService _authService;

        public SessionService(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<SessionResponse> CreateSessionAsync(CreateSessionRequest request)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClientAsync();
                var authResponse = await _authService.GetAuthenticatedUserAsync();
                if (authResponse is not null)
                {
                    var response = await httpClient.PostAsJsonAsync($"users/{authResponse.Id}/sessions", request);
                
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var sessionResponse = JsonSerializer.Deserialize<SessionResponse>(content, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        return sessionResponse!;
                    }
                    else
                    {
                        throw new Exception($"status code: {response.StatusCode}");
                    }
                }
                else
                {
                    throw new Exception($"Auth response is null");
                }
            }
            catch 
            {
                throw;
            }
        }

        public async Task<SessionResponse> UpdateSessionAsync(UpdateSessionRequest request)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClientAsync();
                var authResponse = await _authService.GetAuthenticatedUserAsync();
                if (authResponse is not null)
                {
                    var response = await httpClient.PostAsJsonAsync($"users/{authResponse.Id}/sessions/update", request);
                
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var sessionResponse = JsonSerializer.Deserialize<SessionResponse>(content, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        return sessionResponse!;
                    }
                    else
                    {
                        throw new Exception($"status code: {response.StatusCode}");
                    }
                }
                else
                {
                    throw new Exception($"Auth response is null");
                }
            }
            catch 
            {
                throw;
            }
        }

        public async Task<IEnumerable<SessionResponse>> GetSessionsAsync()
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClientAsync();
                var authResponse = await _authService.GetAuthenticatedUserAsync();
                if (authResponse is not null)
                {
                    var response = await httpClient.GetAsync($"users/{authResponse.Id}/sessions");
                
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var sessionResponse = JsonSerializer.Deserialize<IEnumerable<SessionResponse>>(content, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        return sessionResponse!;
                    }
                    else
                    {
                        throw new Exception($"status code: {response.StatusCode}");
                    }
                }
                else
                {
                    throw new Exception($"Auth response is null");
                }
            }
            catch 
            {
                throw;
            }
        }
    }
}
