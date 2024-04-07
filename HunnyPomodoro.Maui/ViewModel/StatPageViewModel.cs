using CommunityToolkit.Mvvm.ComponentModel;
using HunnyPomodoro.Domain.Session;
using HunnyPomodoro.Maui.Extensions;
using HunnyPomodoro.Maui.Services;
using HunnyPomodoro.Maui.ViewModel.Model;
using Microcharts;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace HunnyPomodoro.Maui.ViewModel
{
    public partial class StatPageViewModel : ObservableObject
    {
        private readonly ISessionService _sessionService;
        public Task Initialization { get; private set; }

        [ObservableProperty]
        private string _totalMinutes ="";

        public ObservableCollection<DisplaySession> WeeklySessions { get; set; } = [];

        [ObservableProperty]
        private BarChart _chart = new();

        public DateTimeOffset KstNow => TimeZoneInfo.ConvertTime(
            DateTimeOffset.Now,
            TimeZoneInfo.FindSystemTimeZoneById(AppConstants.KST));

        public StatPageViewModel(ISessionService sessionService)
        {
            _sessionService = sessionService;

            SetChart();

            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var sessions = await _sessionService.GetSessionsAsync();

            var allSessions = await _sessionService.GetSessionsAsync();
            var doneSessions = allSessions.Where(d => d.Status == SessionStatus.Done.ToString());

            DateTimeOffset startOfWeekDateTimeOffset = KstNow.StartOfWeek(DayOfWeek.Sunday); //{3/31/2024 12:00:00 AM +09:00}
            DateTime startOfWeek = startOfWeekDateTimeOffset.UtcDateTime; //{3/30/2024 3:00:00 PM}
            DateTime endOfWeek = startOfWeek.AddDays(6);

            var weeklySessions = (from DayOfWeek dow in Enum.GetValues(typeof(DayOfWeek))
                                  orderby dow
                                  let sessionCount = doneSessions.Count(d => d.StartTime.DayOfWeek == dow
                                                                          && d.StartTime >= startOfWeek
                                                                          && d.EndTime < endOfWeek)
                                  select new DisplaySession
                                  {
                                      DayOfWeek = dow,
                                      SessionCount = sessionCount,
                                      Mins = sessionCount == 0 ? $"🍅 0 mins"
                                                               : $"🍅 {AppConstants.DEFAULT_TIMER_MINUTES * sessionCount} mins",
                                      Date = $"{startOfWeek.AddDays((int)dow)
                                                           .AddHours(startOfWeekDateTimeOffset.Offset.TotalHours):dddd, MMMM d}"
                                  }).ToList();

            weeklySessions.ForEach(s => WeeklySessions.Add(s));

            var chartEntires = WeeklySessions.Select(s => new ChartEntry(s.SessionCount)
            {
                Label = s.DayOfWeek.ToString().Substring(0, 3).ToUpper(),
                Color = SKColor.Parse("#e03131"),
                ValueLabel = s.SessionCount.ToString()
            });

            Chart.Entries = chartEntires;

            var totalSessionCount = WeeklySessions.Sum(d => d.SessionCount);
            TotalMinutes = totalSessionCount == 0 ? "🍅 0 mins" : $"🍅 {totalSessionCount * AppConstants.DEFAULT_TIMER_MINUTES} mins";
        }

        private void SetChart()
        {
            Chart.IsAnimated = true;
            Chart.LabelOrientation = Orientation.Horizontal;
            Chart.LabelTextSize = 32;
            Chart.LabelColor = SKColor.Parse("#000000");
            Chart.ValueLabelOrientation = Orientation.Horizontal;
            Chart.ValueLabelTextSize = 30;
        }
    }
}
