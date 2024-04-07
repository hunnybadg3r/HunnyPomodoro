using HunnyPomodoro.Maui.View;

namespace HunnyPomodoro.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(TimerPage), typeof(TimerPage));
            Routing.RegisterRoute(nameof(StatPage), typeof(StatPage));
        }
    }
}
