using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryTools.Interfaces.CommonInterfaces;
using StorageModels.Models.UserModels;

namespace RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories
{
    public interface IHospitalUserSectionAccessRepository : IUpdateAbleRepository<HospitalUserSectionAccessStorageModel>
    {
    }
}
