using Enums.Enums;

namespace Enums.EnumExtensions
{
    public static class AgeRangeExtension
    {public static string ToCorrectString(this AgeRange ageRange)
        {
            if (ageRange == AgeRange.Before18)
            {
                return "Младше 18 лет";
            }
            if (ageRange == AgeRange.After18)
            {
                return "18 лет и старше";
            }
            return null;
        }
    }
}
