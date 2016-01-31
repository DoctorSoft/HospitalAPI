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
    }
}
