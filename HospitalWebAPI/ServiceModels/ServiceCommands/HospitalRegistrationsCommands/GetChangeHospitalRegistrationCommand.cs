using System;
using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class GetChangeHospitalRegistrationCommand : AbstractTokenCommand
    {
        public int HospitalProfileId { get; set; }
        public List<HospitalRegistrationCountStatisticItem> FreeHospitalSectionsForRegistration { get; set; }
        public string Date { get; set; }

        // new 

        public int EmptyPlaceByTypeStatisticIdMaleId { get; set; }
        public int EmptyPlaceByTypeStatisticIdFeMaleId { get; set; }
        public string SectionProfileName { get; set; }
        public int CountRegisteredMale { get; set; }
        public int CountRegisteredFemale { get; set; }
    }
}
