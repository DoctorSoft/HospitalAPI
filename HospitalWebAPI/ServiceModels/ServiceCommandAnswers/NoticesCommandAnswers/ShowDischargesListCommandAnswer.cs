using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers
{
    public class ShowDischargesListCommandAnswer : AbstractTokenCommandAnswer
    {
        public List<DischargeFileItem> Files { get; set; }
    }
}
