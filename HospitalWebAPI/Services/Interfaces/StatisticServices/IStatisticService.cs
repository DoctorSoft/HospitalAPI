using ServiceModels.ServiceCommandAnswers.StatisticCommandAnswers;
using ServiceModels.ServiceCommands.StatisticCommands;

namespace Services.Interfaces.StatisticServices
{
    public interface IStatisticService
    {
        GetReviewerStatisticPageInformationCommandAnswer GetReviewerStatisticPageInformation(
            GetReviewerStatisticPageInformationCommand command);
    }
}
