using System;
using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;
using StorageModels.Models.HospitalModels;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class ChangeHospitalRegistrationForNewSectionCommandAnswer : AbstractTokenCommandAnswer
    {
        public List<HospitalSectionProfileStorageModel> FreeHospitalSectionsForRegistration { get; set; }
        public DateTime Date { get; set; }
    }
}
