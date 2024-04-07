using HunnyPomodoro.Maui.ViewModel;

namespace HunnyPomodoro.Maui.View;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}