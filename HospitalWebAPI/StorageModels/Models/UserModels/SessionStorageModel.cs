using System;
using StorageModels.Interfaces;

namespace StorageModels.Models.UserModels
{
    public class SessionStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public DateTime StartTime { get; set; }
        public Guid Token { get; set; }

        //

        public AccountStorageModel Account { get; set; }       
        
        //

        public int AccountId { get; set; }
    }
}
