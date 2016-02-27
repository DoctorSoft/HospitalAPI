using System;

namespace Enums.EnumExtensions
{
    public static class DateTimeExtensions
    {
        public static string GetMonthString(this DateTime date, int plusDays = 0)
        {
            var monthNumber = (date.AddDays(plusDays)).Month - 1;
            string[] months = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
            return months[monthNumber];
        }
    }
}
