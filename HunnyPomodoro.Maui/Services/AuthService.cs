using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using HunnyPomodoro.Contracts.Authentication;

namespace HunnyPomodoro.Maui.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> IsUserAuthenticated()
        {
            var serializedData = await SecureStorage.Default.GetAsync(AppConstants.AuthStorageKeyName);
            return !string.IsNullOrWhiteSpace(serializedData);
        }

        public async Task<AuthenticationResponse?> GetAuthenticatedUserAsync()
        {
            var serializedData = await SecureStorage.Default.GetAsync(AppConstants.AuthStorageKeyName);
            if (!string.IsNullOrWhiteSpace(serializedData))
            {
                return JsonSerializer.Deserialize<AuthenticationResponse>(serializedData);
            }
            else
            {
                return null;
            }
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient(AppConstants.HttpClientName);

            var response = await httpClient.PostAsJsonAsync("auth/login", request);
            var authResponse = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

            if (response.IsSuccessStatusCode && authResponse is not null)
            {
                return authResponse;
            }
            else
            {
                throw new Exception($"status code: {response.StatusCode}");
            }
        }

        public void Logout() => SecureStorage.Default.Remove(AppConstants.AuthStorageKeyName);

        public async Task<HttpClient> GetAuthenticatedHttpClientAsync()
        {
            var httpClient = _httpClientFactory.CreateClient(AppConstants.HttpClientName);

            try
            {
                var authenticatedUser = await GetAuthenticatedUserAsync();

                if (authenticatedUser is not null)
                {
                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authenticatedUser.Token);
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return httpClient;
        }

        public async Task<AuthenticationResponse> SignUpAsync(RegisterRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient(AppConstants.HttpClientName);

            var response = await httpClient.PostAsJsonAsync("auth/register", request);
            var authResponse = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

            if (response.IsSuccessStatusCode && authResponse is not null)
            {
                return authResponse;
            }
            else
            {
                throw new Exception($"status code: {response.StatusCode}");
            }
        }
    }
}
