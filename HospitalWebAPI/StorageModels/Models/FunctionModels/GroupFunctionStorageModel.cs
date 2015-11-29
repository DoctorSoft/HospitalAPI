﻿using StorageModels.Interfaces;

namespace StorageModels.Models.FunctionModels
{
    public class GroupFunctionStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public DistributiveGroupStorageModel DistributiveGroup { get; set; }
        public FunctionStorageModel Function { get; set; }

        //

        public int DistributiveGroupId { get; set; }
        public int FunctionId { get; set; }
    }
}
