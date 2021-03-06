﻿using System.Collections.Generic;
using StorageModels.Interfaces;

namespace StorageModels.Models.UserModels
{
    public class AccountStorageModel : IIdModel, IBlockAbleModel
    {
        public int Id { get; set; }

        public bool IsBlocked { get; set; }

        //

        public string Login { get; set; }

        public string HashedPassword { get; set; }

        //

        public ICollection<SessionStorageModel> Sessions { get; set; }
        public UserStorageModel User { get; set; }

    }
}
