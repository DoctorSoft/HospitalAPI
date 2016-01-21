using System.Collections.Generic;
using ServiceModels.ModelTools;
using StorageModels.Models.HospitalModels;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class ChangeHospitalRegistrationForNewSectionCommandAnswer : AbstractTokenCommandAnswer
    {
        public List<HospitalSectionProfileStorageModel> FreeHospitalSectionsForRegistration { get; set; }
    }
}
