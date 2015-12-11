using System;
using System.Collections.Generic;
using System.Security.Principal;
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
            var resultUserType = _extendedRandom.GetRandomEnumValue<UserAccountType>();

            return new GetUserMainPageInformationCommandAnswer
            {
                UserType = resultUserType,
                Token = (Guid)command.Token
            };
        }

        public GetAdministratorMainPageInformationCommandAnswer GetAdministratorMainPageInformation(
            GetAdministratorMainPageInformationCommand command)
        {
            var answer = new GetAdministratorMainPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };

            return answer;
        }

        public GetClinicUserMainPageInformationCommandAnswer GetClinicUserMainPageInformation(
            GetClinicUserMainPageInformationCommand command)
        {
            var answer = new GetClinicUserMainPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };
            return answer;
        }

        public GetHospitalUserMainPageInformationCommandAnswer GetHospitalUserMainPageInformation(
            GetHospitalUserMainPageInformationCommand command)
        {
            var answer = new GetHospitalUserMainPageInformationCommandAnswer
            {
                Token = (Guid)command.Token
            };
            return answer;
        }

        public GetReviewerMainPageInformationCommandAnswer GetReviewerMainPageInformation(GetReviewerMainPageInformationCommand command)
        {
            var answer = new GetReviewerMainPageInformationCommandAnswer
            {
                Token = (Guid) command.Token
            };
            return answer;
        }
    }
}
