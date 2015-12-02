using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.AnotherRepositories.RemoteAPIRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.HospitalRepositories;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Enums;
using StorageModels.Models.HospitalModels;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class HospitalBotModelCreator : IHospitalBotModelCreator
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IUserTypeRepository _userTypeRepository;

        public HospitalBotModelCreator(IHospitalRepository hospitalRepository, IUserTypeRepository userTypeRepository)
        {
            _hospitalRepository = hospitalRepository;
            _userTypeRepository = userTypeRepository;
        }
   
        public IEnumerable<HospitalUserStorageModel> GetList()
        {
            var hospitals = _hospitalRepository.GetModels().ToList();
            var userTypeId = _userTypeRepository.GetModels().FirstOrDefault(model => model.UserType == UserType.HospitalUser).Id;

            var results = hospitals.Select(model => GetBotByHospital(model, userTypeId));

            return results;
        }

        protected virtual HospitalUserStorageModel GetBotByHospital(HospitalStorageModel hospital, int userTypeId)
        {
            var botUser = new HospitalUserStorageModel
            {
                HospitalId = hospital.Id,
                User = new UserStorageModel
                {
                    Name = string.Format("{0} {1}", hospital.Name, " АвтоРассылка"),
                    UserTypeId = userTypeId,
                }
            };

            return botUser;
        }
    }
}
