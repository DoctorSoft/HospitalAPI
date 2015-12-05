using RepositoryTools.Interfaces.CommonInterfaces;
using StorageModels.Models.ClinicModels;

namespace RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories
{
    public interface IPatientRepository : IAddAbleRepository<PatientStorageModel>
    {
    }
}
