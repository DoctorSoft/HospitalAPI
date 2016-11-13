using System.Collections.Generic;
using System.Linq;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using DataBaseTools.Interfaces;
using Enums.Enums;
using StorageModels.Models.UserModels;

namespace CreateRandomDataTools.DataCreators
{
    public class BotModelCreator : IBotModelCreator
    {
        private readonly IDataBaseContext _context;

        public BotModelCreator(IDataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<UserStorageModel> GetList()
        {
            var userTypeId = _context.Set<UserTypeStorageModel>().FirstOrDefault(model => model.UserType == UserType.Bot).Id;

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
