﻿using CreateRandomDataTools.Interfaces.CommonInterfaces;
using StorageModels.Models.ClinicModels;

namespace CreateRandomDataTools.Interfaces.PrivateInterfaces
{
    public interface IClinicModelCreator: IRandomModelListCreator<ClinicStorageModel>
    {
    }
}
