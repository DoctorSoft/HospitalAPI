using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;
using StorageModels.Models.HospitalModels;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class GetChangeNewHospitalRegistrationCommand : AbstractTokenCommand
    {
        public int HospitalProfileId { get; set; }
        public List<HospitalRegistrationCountStatisticItem> CountFreePlaces { get; set; }
        public List<HospitalSectionProfileStorageModel> FreeHospitalSectionsForRegistration { get; set; }
        public string Date { get; set; }
    }
}
