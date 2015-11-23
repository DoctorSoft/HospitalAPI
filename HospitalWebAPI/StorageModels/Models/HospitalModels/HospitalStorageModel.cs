using StorageModels.Interfaces;

namespace StorageModels.Models.HospitalModels
{
    public class HospitalStorageModel : IIdModel, IBlockableModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //


    }
}
