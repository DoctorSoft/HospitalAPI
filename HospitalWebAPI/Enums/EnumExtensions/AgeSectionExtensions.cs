using Enums.Enums;

namespace Enums.EnumExtensions
{
    public static class AgeSectionExtensions
    {
        public static string ToCorrectString(this AgeSection ageSection)
        {
            if (ageSection == AgeSection.LessThan3)
            {
                return "Младше 3-х лет";
            }

            if (ageSection == AgeSection.Between3And18)
            {
                return "От 3-х до 18-и лет";
            }

            return "Старше 18-и лет";
        }

        public static bool IsCorrectAge(this AgeSection ageSection, int age)
        {
            if (ageSection == AgeSection.LessThan3)
            {
                return age >= 0 && age < 3;
            }

            if (ageSection == AgeSection.Between3And18)
            {
                return age > 3 && age < 18;
            }

            return age > 18;
        }
    }
}
