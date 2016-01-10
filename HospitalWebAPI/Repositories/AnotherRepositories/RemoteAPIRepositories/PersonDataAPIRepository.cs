using System.Collections.Generic;
using System.Linq;
using RemoteServicesTools.Interfaces;
using RepositoryTools.Interfaces.PrivateInterfaces.AnotherRepositories.RemoteAPIRepositories;
using StorageModels.Models.AnotherModels.RemoteAPIModels;

namespace Repositories.AnotherRepositories.RemoteAPIRepositories
{
    public class PersonDataAPIRepository : IPersonDataAPIRepository
    {
        private readonly IAPIDataBrowser _apiDataBrowser;
        private IQueryable<PersonDataAPIStorageModel> _models;
        private const int Count = 100;
        private const string Url = "http://randus.ru/api.php";
        private const string FirstNameKey = "fname";
        private const string LastNameKey = "lname";

        public PersonDataAPIRepository(IAPIDataBrowser apiDataBrowser)
        {
            _apiDataBrowser = apiDataBrowser;
        }

        protected virtual PersonDataAPIStorageModel GetRandomModel()
        {
            var dictionary = _apiDataBrowser.GetData<Dictionary<string, object>>(Url);
            return new PersonDataAPIStorageModel
            {
                FirstName = dictionary[FirstNameKey].ToString().Split(' ').First(),
                LastName = dictionary[LastNameKey].ToString().Split(' ').First()
            };
        }

        protected virtual void LoadRemoteData()
        {
            var results = Enumerable.Range(0, Count)
                .Select(i =>
                {
                    var model = GetRandomModel();
                    model.Id = i + 1;
                    return model;
                });
            _models = results.ToList().AsQueryable();
        }

        public IQueryable<PersonDataAPIStorageModel> GetModels()
        {
            if (_models == null)
            {
                LoadRemoteData();
            }

            return _models;
        }

        public PersonDataAPIStorageModel GetModelById(int id)
        {
            if (id == 0)
            {
                return GetRandomModel();
            }

            var models = GetModels();
            return models.SingleOrDefault(model => model.Id == id);
        }
    }
}
