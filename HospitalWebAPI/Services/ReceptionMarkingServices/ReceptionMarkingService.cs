using System;
using System.Collections.Generic;
using ServiceModels.ServiceCommandAnswers.ReceptionMarkingCommandAnswers;
using ServiceModels.ServiceCommandAnswers.ReceptionMarkingCommandAnswers.Entities;
using ServiceModels.ServiceCommands.ReceptionMarkingCommands;
using Services.Interfaces.ReceptionMarkingServices;

namespace Services.ReceptionMarkingServices
{
    public class ReceptionMarkingService : IReceptionMarkingService
    {
        public GetReceptionUserMarkClientsPageInformationCommandAnswer GetReceptionUserMarkClientsPageInformation(
            GetReceptionUserMarkClientsPageInformationCommand command)
        {
            var table = new List<ReceptionClientTableItem>
            {
                new ReceptionClientTableItem {FirstName = "John", LastName = "Bon Jovi", ClientCode = "12345"},
                new ReceptionClientTableItem {FirstName = "Mark", LastName = "Twen", ClientCode = "22222"},
                new ReceptionClientTableItem {FirstName = "William", LastName = "Schecspire", ClientCode = "66613"},
                new ReceptionClientTableItem {FirstName = "Bob", LastName = "Marley", ClientCode = "00000"},
            };

            var result = new  GetReceptionUserMarkClientsPageInformationCommandAnswer
            {
                Token = (Guid)command.Token,
                Date = DateTime.Now,
                TableItems = table
            };

            return result;
        }

        public GetReceptionUserUnmarkClientsPageInformationCommandAnswer GetReceptionUserUnmarkClientsPageInformation(
            GetReceptionUserUnmarkClientsPageInformationCommand command)
        {

            var result = new GetReceptionUserUnmarkClientsPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };

            return result;
        }
    }
}
