using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.ClinicModels;

namespace CreateRandomDataTools.DataCreators
{
    public class ClinicModelCreator : IClinicModelCreator
    {
        public IEnumerable<ClinicStorageModel> GetList()
        {
           return null; 
        }
    }
}
