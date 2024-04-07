using HunnyPomodoro.Maui.ViewModel;

namespace HunnyPomodoro.Maui.View;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}