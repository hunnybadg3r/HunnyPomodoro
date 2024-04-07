using HunnyPomodoro.Maui.Services;
using HunnyPomodoro.Maui.ViewModel;

namespace HunnyPomodoro.Maui.View;

public partial class TimerPage : ContentPage
{
    private readonly IAuthService _authService;

    public TimerPage(TimerPageViewModel vm, IAuthService authService)
    {
        InitializeComponent();

        BindingContext = vm;
        _authService = authService;
    }

    protected override async void OnAppearing()
    {
        if (await _authService.IsUserAuthenticated() == false)
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
    }
}
