using Enums.Enums;

namespace Enums.EnumExtensions
{
    public static class AgeRangeExtension
    {public static string ToCorrectString(this AgeRange ageRange)
        {
            if (ageRange == AgeRange.MoreOneYear)
            {
                return "Больше года";
            }
            if (ageRange == AgeRange.LessOneYear)
            {
                return "До года";
            }
            return null;
        }
    }
}
