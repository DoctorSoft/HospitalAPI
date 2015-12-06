using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using Enums.Enums;
using RepositoryTools.Interfaces.PrivateInterfaces.UserRepositories;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class BotModelCreator : IBotModelCreator
    {
        private readonly IUserTypeRepository _userTypeRepository;

        public BotModelCreator(IUserTypeRepository userTypeRepository)
        {
            
            _userTypeRepository = userTypeRepository;
        }

        public IEnumerable<UserStorageModel> GetList()
        {
            var userTypeId = _userTypeRepository.GetModels().FirstOrDefault(model => model.UserType == UserType.Bot).Id;

            var bot = GetBot(userTypeId);
            var results = new List<UserStorageModel>
            {
                bot
            };

            return results;
        }

        protected virtual UserStorageModel GetBot(int userTypeId)
        {
            var botUser = new UserStorageModel
            {
                Name = "АвтоРассылка",
                UserTypeId = userTypeId,
            };

            return botUser;
        }
    }
}
