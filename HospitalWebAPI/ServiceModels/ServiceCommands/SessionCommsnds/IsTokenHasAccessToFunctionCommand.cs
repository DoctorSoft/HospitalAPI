using System.Collections.Generic;
using Enums.Enums;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.SessionCommsnds
{
    public class IsTokenHasAccessToFunctionCommand : AbstractTokenCommand
    {
        public List<FunctionIdentityName> Functions { get; set; }
    }
}
