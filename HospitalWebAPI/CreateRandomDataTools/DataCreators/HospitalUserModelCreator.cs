using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using DataBaseTools.Interfaces;
using Enums.Enums;
using HelpingTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.AnotherRepositories.RemoteAPIRepositories;
using StorageModels.Models.HospitalModels;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class HospitalUserModelCreator : IHospitalUserModelCreator
    {
        private readonly IPersonDataAPIRepository _personDataApiRepository;

        private readonly IPasswordHashManager _passwordHashManager;
        private readonly IAccountNameCalculator _accountNameCalculator;
        private readonly IDataBaseContext _context;

        private const int UsersInOneHospital = 2;
        private const string StandardPassword = "12345";

        public HospitalUserModelCreator(IPersonDataAPIRepository personDataApiRepository, 
            IPasswordHashManager passwordHashManager, IAccountNameCalculator accountNameCalculator, IDataBaseContext context)
        {
            _personDataApiRepository = personDataApiRepository;
            _passwordHashManager = passwordHashManager;
            _accountNameCalculator = accountNameCalculator;
            _context = context;
        }
   
        public IEnumerable<HospitalUserStorageModel> GetList()
        {
            var hospital = _context.Set<HospitalStorageModel>().FirstOrDefault();
            var userTypeId = _context.Set<UserTypeStorageModel>().FirstOrDefault(model => model.UserType == UserType.HospitalUser).Id;

            var results = GetUsersByHospital(hospital.Id, userTypeId);

            return results;
        }

        protected virtual IEnumerable<HospitalUserStorageModel> GetUsersByHospital(int hospitalId, int userTypeId)
        {
            return new List<HospitalUserStorageModel>
            {
                new HospitalUserStorageModel
                {
                    Id = 0,
                    HospitalId = hospitalId,
                    User = new UserStorageModel
                    {
                        Account = new AccountStorageModel
                        {
                            Id = 0,
                            HashedPassword = _passwordHashManager.GetPasswordHash("Nice90Park"),
                            IsBlocked = false,
                            Login = "Hospital3Test1"
                        },
                        Id = 0,
                        Name = "Hospital 3 User 1",
                        UserTypeId = userTypeId
                    }
                },
                new HospitalUserStorageModel
                {
                    Id = 0,
                    HospitalId = hospitalId,
                    User = new UserStorageModel
                    {
                        Account = new AccountStorageModel
                        {
                            Id = 0,
                            HashedPassword = _passwordHashManager.GetPasswordHash("Velocity45Speed"),
                            IsBlocked = false,
                            Login = "Hospital3Test2"
                        },
                        Id = 0,
                        Name = "Hospital 3 User 2",
                        UserTypeId = userTypeId
                    }
                }
            };
        }
    }
}
