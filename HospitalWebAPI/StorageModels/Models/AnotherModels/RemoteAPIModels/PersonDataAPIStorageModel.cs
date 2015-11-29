using StorageModels.Interfaces;

namespace StorageModels.Models.AnotherModels.RemoteAPIModels
{
    public class PersonDataAPIStorageModel : IIdModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
