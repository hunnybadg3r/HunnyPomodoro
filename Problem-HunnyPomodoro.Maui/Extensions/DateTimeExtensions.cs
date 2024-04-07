using System.Runtime.CompilerServices;

namespace HunnyPomodoro.Maui.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTimeOffset StartOfWeek(this DateTimeOffset dto, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dto.DayOfWeek - startOfWeek)) % 7;
            return dto.AddDays(-1 * diff).Add(-dto.TimeOfDay);
        }

        public static DateTimeOffset ConvertToKst(this DateTime dt)
        {
            return TimeZoneInfo.ConvertTime(dt, TimeZoneInfo.FindSystemTimeZoneById(AppConstants.KST));
        }
    }
}