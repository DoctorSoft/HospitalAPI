using System.Collections.Generic;
using Enums.Enums;
using HelpingTools.Interfaces;
using ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers;
using ServiceModels.ServiceCommands.MainPageCommands;
using Services.Interfaces.MainPageServices;

namespace Services.MainPageServices
{
    public class MainPageService : IMainPageService
    {
        private readonly IExtendedRandom _extendedRandom;

        public MainPageService(IExtendedRandom extendedRandom)
        {
            _extendedRandom = extendedRandom;
        }

        // TODO: Implement this method
        public GetUserMainPageInformationCommandAnswer GetUserMainPageInformation(GetUserMainPageInformationCommand command)
        {
            var resultUserType = _extendedRandom.GetRandomValueFromList(new List<UserType>
            {
                UserType.ClinicUser, UserType.HospitalUser, UserType.Administrator, UserType.Reviewer
            });

            return new GetUserMainPageInformationCommandAnswer
            {
                UserType = resultUserType
            };
        }
    }
}
