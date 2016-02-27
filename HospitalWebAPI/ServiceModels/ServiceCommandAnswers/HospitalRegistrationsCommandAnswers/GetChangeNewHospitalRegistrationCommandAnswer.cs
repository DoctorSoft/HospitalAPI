using System;
using System.Collections.Generic;
using ServiceModels.ModelTools;
using StorageModels.Models.HospitalModels;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class GetChangeNewHospitalRegistrationCommandAnswer : AbstractTokenCommandAnswer
    {
        public string Date { get; set; }
        public List<HospitalSectionProfileStorageModel> FreeHospitalSectionsForRegistration { get; set; }
    }
}
