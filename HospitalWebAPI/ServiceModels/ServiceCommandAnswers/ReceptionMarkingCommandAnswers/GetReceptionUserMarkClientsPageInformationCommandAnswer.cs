using System;
using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.ReceptionMarkingCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.ReceptionMarkingCommandAnswers
{
    public class GetReceptionUserMarkClientsPageInformationCommandAnswer : AbstractTokenCommandAnswer
    {
        public DateTime Date { get; set; }

        public List<ReceptionClientTableItem> TableItems { get; set; }
    }
}
