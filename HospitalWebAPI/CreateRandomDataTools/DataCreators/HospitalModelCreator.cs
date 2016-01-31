using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.HospitalModels;

namespace CreateRandomDataTools.DataCreators
{
    public class HospitalModelCreator : IHospitalModelCreator
    {
        public IEnumerable<HospitalStorageModel> GetList()
        {
            var hospitalsList = new List<HospitalStorageModel>
            {
                new HospitalStorageModel
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Гродненская областная детская клиническая больница",
                    Address = "г. Гродно, ул. Островского, 22"
                },
                new HospitalStorageModel
                {
                    IsBlocked = false,
                    Id = 1,
                    Name = "Гродненская городская клиническая больница №1",
                    Address = "г. Гродно, ул. Коммунальная, 2"
                },
                /*new HospitalStorageModel
                {
                    IsBlocked = false,
                    Id = 2,
                    Name = "Гродненский областной клинический перинатальный центр",
                    Address = "г. Гродно, ул. Горького, 77"
                },
                new HospitalStorageModel
                {
                    IsBlocked = false,
                    Id = 3,
                    Name = "Гродненский областной кардиологический центр",
                    Address = "г. Гродно, ул. Болдина, 9"
                },
                new HospitalStorageModel
                {
                    IsBlocked = false,
                    Id = 4,
                    Name = "Гродненская городская клиническая больница №2",
                    Address = "г. Гродно, ул. Гагарина, 5"
                },
                new HospitalStorageModel
                {
                    IsBlocked = false,
                    Id = 5,
                    Name = "Гродненская городская клиническая больница №3",
                    Address = "г. Гродно, б-р Ленинского Комсомола, 59"
                },
                new HospitalStorageModel
                {
                    IsBlocked = false,
                    Id = 6,
                    Name = "Гродненская городская клиническая больница №4",
                    Address = "г. Гродно, пр-т Я. Купалы, 89"
                },
                new HospitalStorageModel
                {
                    IsBlocked = false,
                    Id = 7,
                    Name = "Гродненская областная клиническая больница",
                    Address = "г. Гродно, б-р Ленинского Комсомола, 52"
                },
                new HospitalStorageModel
                {
                    IsBlocked = false,
                    Id = 8,
                    Name = "Гродненская городская больница скорой помощи",
                    Address = "г. Гродно, ул. Сов. Пограничников, 115"
                },
                new HospitalStorageModel
                {
                    IsBlocked = false,
                    Id = 9,
                    Name = "Военный госпиталь",
                    Address = "г. Гродно, ул. Дзержинского, 17"
                }*/
            };

            return hospitalsList;
        }
    }
}
