using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.AuthorizationCommandAnswers
{
    public class GetTokenByUserCredentialsCommandAnswer : AbstractCommandAnswer
    {
        public Guid Token { get; set; }
    }
}
