using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HunnyPomodoro.Contracts.Authentication;
using HunnyPomodoro.Maui.Services;
using HunnyPomodoro.Maui.View;
using HunnyPomodoro.Validation;
using System.Text.Json;

namespace HunnyPomodoro.Maui.ViewModel
{
    public partial class LoginPageViewModel : ObservableObject
    {
        [ObservableProperty]
        ValidatableObject<string> _email = new();
        [ObservableProperty]
        ValidatableObject<string> _password = new();

        private readonly IAuthService _authService;
        private readonly IAlertService _alertService;

        public LoginPageViewModel(IAuthService authService, IAlertService alertService)
        {
            _authService = authService;
            _alertService = alertService;

            AddValidations();
        }

        [RelayCommand]
        Task CreateAccount() =>
            Shell.Current.GoToAsync($"{nameof(SignUpPage)}");

        [RelayCommand]
        async Task Login()
        {
            var request = new LoginRequest(Email.Value, Password.Value);

            try
            {
                var response = await _authService.LoginAsync(request);

                var serializedResponse = JsonSerializer.Serialize(response);
                await SecureStorage.Default.SetAsync(AppConstants.AuthStorageKeyName, serializedResponse);

                await Shell.Current.GoToAsync($"{nameof(TimerPage)}");
            }
            catch 
            {
                _alertService.ShowAlert("error", "Incorrect username or password");
            }
        }

        private bool Validate()
        {
            return ValidateObject(Email)
                && ValidateObject(Password);
        }

        private bool ValidateObject<T>(ValidatableObject<T> obj)
        {
            return obj.Validate();
        }

        private void AddValidations()
        {
            Email.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "A Email is required."
            });
            
            Email.Validations.Add(new EmailRule<string>
            {
                ValidationMessage = "Invalid email address format."
            });

            Password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "A password is required."
            });
        }
    }
}
