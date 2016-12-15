using System.Collections.Generic;
using ServiceModels.ModelTools;
using StorageModels.Models.HospitalModels;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class GetChangeNewHospitalRegistrationCommandAnswer : AbstractMessagedCommandAnswer
    {
        public string Date { get; set; }
        public List<HospitalSectionProfileStorageModel> FreeHospitalSectionsForRegistration { get; set; }
    }
}
