using ServiceModels.ServiceCommandAnswers.StatisticCommandAnswers;
using ServiceModels.ServiceCommands.StatisticCommands;
using Services.Interfaces.StatisticServices;

namespace Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        public GetReviewerStatisticPageInformationCommandAnswer GetReviewerStatisticPageInformation(
            GetReviewerStatisticPageInformationCommand command)
        {
            return new GetReviewerStatisticPageInformationCommandAnswer
            {
                Token = command.Token.Value
            };
        }
    }
}
