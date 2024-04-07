using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using HunnyPomodoro.Contracts.Authentication;
using HunnyPomodoro.Maui.Services;
using HunnyPomodoro.Maui.View;
using HunnyPomodoro.Validation;
using System.Text.Json;

namespace HunnyPomodoro.Maui.ViewModel
{
    public partial class SignUpPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ValidatableObject<string> _email = new();
        [ObservableProperty]
        private ValidatableObject<string> _name = new();
        [ObservableProperty]
        private ValidatableObject<string> _password = new();
        [ObservableProperty]
        private ValidatableObject<string> _confirmPassword = new();

        private bool _isValidAll = false;

        private readonly IAuthService _authService;
        private readonly IAlertService _alertService;

        public SignUpPageViewModel(IAuthService authService, IAlertService alertService)
        {
            _authService = authService;
            _alertService = alertService;

            AddValidations();
        }

        [RelayCommand]
        async Task SignUp()
        {
            if (Validate() == false)
            {
                return;
            }

            var request = new RegisterRequest(Name.Value, Email.Value, Password.Value);
            
            try
            {
                var response = await _authService.SignUpAsync(request);

                var serializedResponse = JsonSerializer.Serialize(response);
                await SecureStorage.Default.SetAsync(AppConstants.AuthStorageKeyName, serializedResponse);

                await Shell.Current.GoToAsync($"{nameof(TimerPage)}");
            }
            catch (Exception ex)
            {
                _alertService.ShowAlert("error", ex.Message);
            }
        }

        private bool Validate()
        {
            return ValidateObject(Email)
                && ValidateObject(Name)
                && ValidateObject(Password)
                && ValidateObject(ConfirmPassword);
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

            Name.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "A name is required."
            });

            Name.Validations.Add(new StringLengthRule<string>(6, 30)
            {
                ValidationMessage = "String length must be between 6 and 30 characters."
            });

            Password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "A password is required."
            });

            ConfirmPassword.Validations.Add(new EqualRule<string>(Password)
            {
                ValidationMessage = "The password confirmation does not match."
            });

        }
    }
}
