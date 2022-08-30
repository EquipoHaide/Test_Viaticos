
using System;

namespace Dominio.Nucleo
{
    public static class DateTimeExtensions
    {
        public static DateTime StartDay (this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        public static DateTime EndDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

    }
}
