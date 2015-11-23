using System.Collections.Generic;
using StorageModels.Interfaces;

namespace StorageModels.Models.UserModels
{
    public class AccountStorageModel : IIdModel, IBlockableModel
    {
        public int Id { get; set; }

        public bool IsBlocked { get; set; }

        //

        public string Login { get; set; }

        public string HashedPassword { get; set; }

        //

        public IEnumerable<SessionStorageModel> Sessions { get; set; }
        public UserStorageModel User { get; set; }

    }
}
