using HunnyPomodoro.Maui.ViewModel;

namespace HunnyPomodoro.Maui.View;

public partial class StatPage : ContentPage
{
	public StatPage(StatPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}
}