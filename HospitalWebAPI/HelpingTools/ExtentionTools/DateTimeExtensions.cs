using System;

namespace HelpingTools.ExtentionTools
{
    public static class DateTimeExtensions
    {
        public static string ToCorrectDateString(this DateTime date)
        {
            return date.ToString("dd.MM.yyyy");
        }
    }
}
