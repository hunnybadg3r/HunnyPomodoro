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

        private int _elapsedSeconds = 0;
        private bool _isDone = false;
        string _sessionId = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CancelTimerCommand))]
        private bool _canCancelTimer = false;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(NavigateStatCommand))]
        private bool _canNavigateStat = true;

        [ObservableProperty]
        private AuthenticationResponse? _authUser;

        [ObservableProperty]
        private int _progress = 0;

        [ObservableProperty]
        private string _timeCount = "";

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

            TimeCount = TimeSpan.FromMinutes(AppConstants.DEFAULT_TIMER_MINUTES)
                                                    .Add(-TimeSpan.FromSeconds(_elapsedSeconds))
                                                    .ToString(@"mm\:ss");
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
                CanNavigateStat = false;
                var startTime = DateTime.UtcNow;

                _cancellationTokenSource = new();

                _elapsedSeconds = 0;
                Progress = 0;

                //Create session and save 
                var request = new CreateSessionRequest(startTime);

                // 1. Create a pomodoro session row in the db
                try
                {
                    var sessionResponse = await _sessionService.CreateSessionAsync(request);
                    _sessionId = sessionResponse.Id;
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
                                _isDone = true;
                                _cancellationTokenSource.Cancel();
                            }
                            else
                            {
                                _elapsedSeconds++;
                                Progress = UpdateProgress();
                                TimeCount = TimeSpan.FromMinutes(AppConstants.DEFAULT_TIMER_MINUTES)
                                                    .Add(-TimeSpan.FromSeconds(_elapsedSeconds))
                                                    .ToString(@"mm\:ss");
                            }
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Canceld");
                    if (!_isDone)
                    {
                        return;
                    }
                }
                catch
                {
                    throw;
                }

                // 3. Update the pomodoro session row in the db
                try
                {
                    var updateRequest = new UpdateSessionRequest(_sessionId, DateTime.UtcNow, SessionStatus.Done.ToString());
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
                _isDone = false;
                CanCancelTimer = false;
                CanNavigateStat = true;
            }

            int UpdateProgress()
            {
                return (int)((_elapsedSeconds / TimeSpan.FromMinutes(AppConstants.DEFAULT_TIMER_MINUTES).TotalSeconds) * 100);
            }
        }


        [RelayCommand(CanExecute = nameof(CanCancelTimer))]
        async Task CancelTimer()
        {
            if (await _alertService.ShowConfirmationAsync("Cancel", "Are you sure you want to cancel this session?"))
            {
                await _cancellationTokenSource.CancelAsync();
                
                TimeCount = "Cancelled";

                // TODO UPDATE SESSION TO CANCELLED"
                //var updateRequest = new UpdateSessionRequest(_sessionId, DateTime.UtcNow, SessionStatus.Cancel.ToString());
                //var response = await _sessionService!.UpdateSessionAsync(updateRequest);

                //Console.WriteLine($"{response.Id} | {response.EndTime} | {response.Status}");
            }
            else
            {
                return;
            }
        }

        [RelayCommand(CanExecute = nameof(CanNavigateStat))]
        Task NavigateStat() =>
            Shell.Current.GoToAsync($"{nameof(StatPage)}");
    }
}
