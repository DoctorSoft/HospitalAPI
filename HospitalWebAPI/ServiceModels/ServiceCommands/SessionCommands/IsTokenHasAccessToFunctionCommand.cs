using System.Collections.Generic;
using Enums.Enums;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.SessionCommands
{
    public class IsTokenHasAccessToFunctionCommand : AbstractTokenCommand
    {
        public List<FunctionIdentityName> Functions { get; set; }
    }
}
