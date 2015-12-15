using Enums.Enums;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.SessionCommandAnswers
{
    public class IsTokenHasAccessToFunctionCommandAnswer : AbstractTokenCommandAnswer
    {
        public AccessType AccessType { get; set; }
    }
}
