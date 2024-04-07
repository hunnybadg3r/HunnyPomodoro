using HunnyPomodoro.Domain.Session;

namespace HunnyPomodoro.Maui.ViewModel.Model
{
    public class DisplaySession
    {
        public required DayOfWeek DayOfWeek { get; set; }
        public required int SessionCount { get; set; }
        public required string Mins { get; set; }
        public required string Date { get; set; }
    }
}
