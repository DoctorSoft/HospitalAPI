using System;
using ServiceModels.ServiceCommandAnswers.ReceptionMarkingCommandAnswers;
using ServiceModels.ServiceCommands.ReceptionMarkingCommands;
using Services.Interfaces.ReceptionMarkingServices;

namespace Services.ReceptionMarkingServices
{
    public class ReceptionMarkingService : IReceptionMarkingService
    {
        public GetReceptionUserMarkClientsPageInformationCommandAnswer GetReceptionUserMarkClientsPageInformation(
            GetReceptionUserMarkClientsPageInformationCommand command)
        {
            var result = new GetReceptionUserMarkClientsPageInformationCommandAnswer
            {
                Token = (Guid) command.Token
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
