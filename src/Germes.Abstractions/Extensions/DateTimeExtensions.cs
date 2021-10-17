using System;

namespace Germes.Abstractions.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsBetween(this DateTime date, DateTime @from, DateTime to)
            => @from <= date && date <= to;

        public static DateTime GetFirstDateOfMonth(this DateTime date)
            => new DateTime(date.Year, date.Month, 1);
    }
}