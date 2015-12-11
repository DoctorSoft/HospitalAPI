using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.SessionCommandAnswers
{
    public class IsTokenHasAccessToFunctionCommandAnswer : AbstractTokenCommandAnswer
    {
        public bool HasAccess { get; set; }
    }
}
