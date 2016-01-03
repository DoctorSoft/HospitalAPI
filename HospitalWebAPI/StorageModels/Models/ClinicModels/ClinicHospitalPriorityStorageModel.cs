using StorageModels.Interfaces;
using StorageModels.Models.HospitalModels;

namespace StorageModels.Models.ClinicModels
{
    public class ClinicHospitalPriorityStorageModel : IIdModel, IBlockAbleModel
    {
        public int Id { get; set; }

        public bool IsBlocked { get; set; }

        //

        public ClinicStorageModel Clinic { get; set; }

        public HospitalStorageModel Hospital { get; set; }

        public int Priority { get; set; }

        //

        public int ClinicId { get; set; }

        public int HospitalId { get; set; }
    }
}
