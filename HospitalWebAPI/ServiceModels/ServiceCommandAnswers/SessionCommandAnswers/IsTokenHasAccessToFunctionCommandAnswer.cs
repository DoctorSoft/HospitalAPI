using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.SessionCommandAnswers
{
    public class IsTokenHasAccessToFunctionCommandAnswer : AbstractCommandAnswer
    {
        public bool HasAccess { get; set; }
    }
}
