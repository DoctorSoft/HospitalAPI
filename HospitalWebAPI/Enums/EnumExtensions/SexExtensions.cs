using Enums.Enums;

namespace Enums.EnumExtensions
{
    public static class SexExtensions
    {
        public static string ToCorrectString(this Sex sex)
        {
            if (sex == Sex.Male)
            {
                return "Мужской";
            }
            if (sex == Sex.Female)
            {
                return "Женский";
            }
            return null;
        }

        public static Sex GetOppositeSex(this Sex sex)
        {
            return sex == Sex.Female ? Sex.Male : Sex.Female;
        }
    }
}
