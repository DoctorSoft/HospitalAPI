using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class GetChangeHospitalRegistrationsPageInformationCommand : AbstractTokenCommand
    {
        public List<HospitalRegistrationCountStatisticItem> StatisticItems { get; set; }
    }
}
