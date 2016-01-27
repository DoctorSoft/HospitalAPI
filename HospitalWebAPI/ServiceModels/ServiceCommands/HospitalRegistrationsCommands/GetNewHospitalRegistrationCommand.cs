using System;
using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class GetNewHospitalRegistrationCommand : AbstractTokenCommand
    {
        public List<HospitalRegistrationCountStatisticItem> FreeHospitalSectionsForRegistration { get; set; }
        public string Date { get; set; }
    }
}
