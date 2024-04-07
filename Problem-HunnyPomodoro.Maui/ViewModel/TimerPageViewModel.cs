using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HunnyPomodoro.Contracts.Authentication;
using HunnyPomodoro.Maui.Services;
using HunnyPomodoro.Maui.View;
using HunnyPomodoro.Contracts.Session;
using HunnyPomodoro.Domain.Session;

namespace HunnyPomodoro.Maui.ViewModel
{
    public partial class TimerPageViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly ISessionService _sessionService;
        private readonly IAlertService _alertService;

        private CancellationTokenSource _cancellationTokenSource = new();

        private const int DEFAULT_TIMER_MINUTES = 1;

        private int _elapsedSeconds = 0;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CancelTimerCommand))]
        private bool _canCancelTimer = false;

        [ObservableProperty]
        private AuthenticationResponse? _authUser;

        [ObservableProperty]
        private int _progress = 0;

        [ObservableProperty]
        private string _timeCount;

        public Task Initialization { get; private set; }

        public TimerPageViewModel(
            IAuthService authService,
            IAlertService alertService,
            ISessionService sessionService)
        {
            _authService = authService;
            _alertService = alertService;
            _sessionService = sessionService;

            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            AuthUser = await _authService.GetAuthenticatedUserAsync();
        }

        [RelayCommand]
        async Task Logout()
        {
            if (await _alertService.ShowConfirmationAsync("Logout", "Are you sure you want to log out?"))
            {
                _authService.Logout();
                await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
            }
            else
            {
                return;
            }
        }

        [RelayCommand]
        async Task StartTimer()
        {
            try
            {
                CanCancelTimer = true;
                var startTime = DateTime.UtcNow;

                _cancellationTokenSource = new();

                string sessionId = "";
                _elapsedSeconds = 0;
                Progress = 0;

                //Create session and save 
                var request = new CreateSessionRequest(startTime);

                // 1. Create a pomodoro session row in the db
                try
                {
                    var sessionResponse = await _sessionService.CreateSessionAsync(request);
                    sessionId = sessionResponse.Id;
                }
                catch
                {
                    throw;
                }

                // 2. Start timer
                var cancellationToken = _cancellationTokenSource.Token;
                try
                {
                    using (var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000)))
                    {
                        while (await timer.WaitForNextTickAsync(cancellationToken))
                        {
                            if (Progress >= 100)
                            {
                                _cancellationTokenSource.Cancel();
                            }
                            else
                            {
                                _elapsedSeconds++;
                                Progress = UpdateProgress();
                                TimeCount = TimeSpan.FromMinutes(DEFAULT_TIMER_MINUTES)
                                                    .Add(-TimeSpan.FromSeconds(_elapsedSeconds))
                                                    .ToString(@"mm\:ss");
                            }
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Canceld");
                }
                catch
                {
                    throw;
                }

                // 3. Update the pomodoro session row in the db
                try
                {
                    var updateRequest = new UpdateSessionRequest(sessionId, DateTime.UtcNow, SessionStatus.Done.ToString());
                    var response = await _sessionService!.UpdateSessionAsync(updateRequest);

                    Console.WriteLine($"{response.Id} | {response.EndTime} | {response.Status}");
                }
                catch
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _alertService.ShowAlert("error", ex.Message);
            }
            finally
            {
                CanCancelTimer = false;
            }

            int UpdateProgress()
            {
                return (int)((_elapsedSeconds / TimeSpan.FromMinutes(DEFAULT_TIMER_MINUTES).TotalSeconds) * 100);
            }
        }


        [RelayCommand(CanExecute = nameof(CanCancelTimer))]
        async Task CancelTimer()
        {
            if (await _alertService.ShowConfirmationAsync("Cancel", "Are you sure you want to cancel this session?"))
            {
                await _cancellationTokenSource.CancelAsync();
                //db에 취소 기록 

            }
            else
            {
                return;
            }
        }

        [RelayCommand]
        Task NavigateStat() =>
            Shell.Current.GoToAsync($"{nameof(StatPage)}");
    }
}
