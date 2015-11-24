using StorageModels.Interfaces;
using StorageModels.Models.ClinicModels;

namespace StorageModels.Models.UserModels
{
    public class ClinicUserStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public UserStorageModel User { get; set; }
        public ClinicStorageModel Clinic { get; set; }

    }
}
