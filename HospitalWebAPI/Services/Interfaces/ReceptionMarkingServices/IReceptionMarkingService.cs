using ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers;
using ServiceModels.ServiceCommandAnswers.ReceptionMarkingCommandAnswers;
using ServiceModels.ServiceCommands.MainPageCommands;
using ServiceModels.ServiceCommands.ReceptionMarkingCommands;

namespace Services.Interfaces.ReceptionMarkingServices
{
    public interface IReceptionMarkingService
    {
        GetReceptionUserMarkClientsPageInformationCommandAnswer GetReceptionUserMarkClientsPageInformation(
            GetReceptionUserMarkClientsPageInformationCommand command);

        GetReceptionUserUnmarkClientsPageInformationCommandAnswer GetReceptionUserUnmarkClientsPageInformation(
            GetReceptionUserUnmarkClientsPageInformationCommand command);
    }
}
