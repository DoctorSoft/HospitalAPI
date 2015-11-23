using StorageModels.Interfaces;

namespace StorageModels.Models.ClinicModels
{
    public class ClinicStorageModel : IIdModel, IBlockableModel
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }

        //

    }
}
