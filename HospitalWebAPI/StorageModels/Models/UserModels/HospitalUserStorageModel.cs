using StorageModels.Interfaces;
using StorageModels.Models.HospitalModels;

namespace StorageModels.Models.UserModels
{
    public class HospitalUserStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public UserStorageModel User { get; set; }
        public HospitalStorageModel Hospital { get; set; }

        //

        public int HospitalId { get; set; }
    }
}
