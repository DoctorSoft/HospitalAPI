﻿using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;

namespace Services.Interfaces.ClinicRegistrationsServices
{
    public interface IClinicRegistrationsService
    {
        GetBreakClinicRegistrationsPageInformationCommandAnswer GetBreakClinicRegistrationsPageInformation(
            GetBreakClinicRegistrationsPageInformationCommand command);

        GetMakeClinicRegistrationsPageInformationCommandAnswer GetMakeClinicRegistrationsPageInformation(
            GetMakeClinicRegistrationsPageInformationCommand command);
    }
}