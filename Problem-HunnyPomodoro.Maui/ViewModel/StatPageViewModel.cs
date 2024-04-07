using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using HunnyPomodoro.Maui.Extensions;
using HunnyPomodoro.Maui.Services;

using Microcharts;

using SkiaSharp;

namespace HunnyPomodoro.Maui.ViewModel
{
    public partial class StatPageViewModel : ObservableObject
    {
        private readonly ISessionService _sessionService;
        public Task Initialization { get; private set; }

        [ObservableProperty]
        private LineChart _chart = new();

        public DateTimeOffset KstNow => TimeZoneInfo.ConvertTime(
            DateTimeOffset.Now,
            TimeZoneInfo.FindSystemTimeZoneById(AppConstants.KST));

        public StatPageViewModel(ISessionService sessionService)
        {
            _sessionService = sessionService;

            //_chart.Entries = [
            //    new ChartEntry(212)
            //    {
            //        Label = "Windows",
            //        ValueLabel = "112",
            //        Color = SKColor.Parse("#2c3e50")
            //    },
            //    new ChartEntry(248)
            //    {
            //        Label = "Android",
            //        ValueLabel = "648",
            //        Color = SKColor.Parse("#77d065")
            //    },
            //    new ChartEntry(128)
            //    {
            //        Label = "iOS",
            //        ValueLabel = "428",
            //        Color = SKColor.Parse("#b455b6")
            //    },
            //    new ChartEntry(514)
            //    {
            //        Label = ".NET MAUI",
            //        ValueLabel = "214",
            //        Color = SKColor.Parse("#3498db")
            //    }];

            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var allSessions = await _sessionService.GetSessionsAsync();

            DateTimeOffset startOfWeekDateTimeOffset = KstNow.StartOfWeek(DayOfWeek.Sunday); //{3/31/2024 12:00:00 AM +09:00}
            DateTime startOfWeek = startOfWeekDateTimeOffset.UtcDateTime; //{3/30/2024 3:00:00 PM}
            DateTime endOfWeek = startOfWeek.AddDays(6);

            var weeklySessions = allSessions
                .Where(d => d.StartTime >= startOfWeek && d.EndTime < endOfWeek)
                .GroupBy(d => d.StartTime.DayOfWeek)
                .Select(g => new { DayOfWeek = g.Key, SessionCount = g.Count() })
                .OrderBy(o => o.DayOfWeek)
                .ToList();

            //var chartEntires = weeklySessions.Select(s => new ChartEntry(s.SessionCount)
            //{
            //    Label = s.DayOfWeek.ToString(),
            //    Color = SKColor.Parse("#ffc9c9"),
            //    ValueLabel = s.SessionCount.ToString(),
            //    OtherColor = SKColor.Parse("#e03131")
            //});

            //Chart.Entries = chartEntires;

            Chart.Entries = new List<ChartEntry>() {new ChartEntry(212){
                Label = "Windows",
                Color = SKColor.Parse("#2c3e50")
            }};
        }


        // TODO delete after test
        [RelayCommand]
    async Task GetSessions()
    {
        var allSessions = await _sessionService.GetSessionsAsync();

        DateTimeOffset startOfWeekDateTimeOffset = KstNow.StartOfWeek(DayOfWeek.Sunday); //{3/31/2024 12:00:00 AM +09:00}
        DateTime startOfWeek = startOfWeekDateTimeOffset.UtcDateTime; //{3/30/2024 3:00:00 PM}
        DateTime endOfWeek = startOfWeek.AddDays(6);

        var weeklySessions = allSessions
            .Where(d => d.StartTime >= startOfWeek && d.EndTime < endOfWeek)
            .GroupBy(d => d.StartTime.DayOfWeek)
            .Select(g => new { DayOfWeek = g.Key, SessionCount = g.Count() })
            .OrderBy(o => o.DayOfWeek)
            .ToList();

        var chartEntires = weeklySessions.Select(s => new ChartEntry(s.SessionCount)
        {
            Label = s.DayOfWeek.ToString(),
            Color = SKColor.Parse("#ffc9c9"),
            ValueLabel = s.SessionCount.ToString(),
            OtherColor = SKColor.Parse("#e03131")
        });


    }
}
}
