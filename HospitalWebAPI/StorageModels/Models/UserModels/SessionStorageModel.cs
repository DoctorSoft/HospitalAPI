using System;
using StorageModels.Interfaces;

namespace StorageModels.Models.UserModels
{
    public class SessionStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public Guid Token { get; set; }
    }
}
