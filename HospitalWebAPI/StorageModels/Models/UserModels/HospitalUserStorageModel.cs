using StorageModels.Interfaces;

namespace StorageModels.Models.UserModels
{
    public class HospitalUserStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public UserStorageModel User { get; set; }
    }
}
