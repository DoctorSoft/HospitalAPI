using System.Collections.Generic;
using StorageModels.Interfaces;
using StorageModels.Models.HospitalModels;
using StorageModels.Models.UserModels;

namespace StorageModels.Models.ClinicModels
{
    public class ClinicUserHospitalSectionProfileAccessStorageModel : IIdModel, IBlockAbleModel
    {
        public int Id { get; set; }

        public bool IsBlocked { get; set; }

        //

        public ClinicUserStorageModel ClinicUser { get; set; }

        public HospitalSectionProfileStorageModel HospitalSectionProfile { get; set; }
        
        //

        public int ClinicUserId { get; set; }

        public int HospitalSectionProfileId { get; set; }
    }
}
