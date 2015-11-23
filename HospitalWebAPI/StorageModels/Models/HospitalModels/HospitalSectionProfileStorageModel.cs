using StorageModels.Interfaces;

namespace StorageModels.Models.HospitalModels
{
    public class HospitalSectionProfileStorageModel : IIdModel, IBlockableModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //
    }
}
