using HelpingTools.Interfaces;

namespace HelpingTools.CalculationTools
{
    public class AccountNameCalculator : IAccountNameCalculator
    {
        private readonly IExtendedRandom _random;

        public AccountNameCalculator(IExtendedRandom random)
        {
            _random = random;
        }

        public string GetAccountName(string firstName, string lastName)
        {
            var index = _random.Next(1000);
            return string.Format("{0}{1}{2}", firstName, lastName, index);
        }
    }
}
