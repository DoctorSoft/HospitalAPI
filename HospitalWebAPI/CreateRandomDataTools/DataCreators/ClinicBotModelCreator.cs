using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using Enums.Enums;
using RepositoryTools.Interfaces.PrivateInterfaces.ClinicRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class ClinicBotModelCreator : IClinicBotModelCreator
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly IUserTypeRepository _userTypeRepository;

        public ClinicBotModelCreator(IClinicRepository clinicRepository, IUserTypeRepository userTypeRepository)
        {
            _clinicRepository = clinicRepository;
            _userTypeRepository = userTypeRepository;
        }
   
        public IEnumerable<ClinicUserStorageModel> GetList()
        {
            var clinics = _clinicRepository.GetModels().ToList();
            var userTypeId = _userTypeRepository.GetModels().FirstOrDefault(model => model.UserType == UserType.Bot).Id;

            var results = clinics.Select(model => GetBotByClinic(model, userTypeId));

            return results;
        }

        protected virtual ClinicUserStorageModel GetBotByClinic(ClinicStorageModel clinic, int userTypeId)
        {
            var botUser = new ClinicUserStorageModel
            {
                ClinicId = clinic.Id,
                User = new UserStorageModel
                {
                    Name = string.Format("{0} {1}", clinic.Name, " АвтоРассылка"),
                    UserTypeId = userTypeId,
                }
            };

            return botUser;
        }
    }
}
