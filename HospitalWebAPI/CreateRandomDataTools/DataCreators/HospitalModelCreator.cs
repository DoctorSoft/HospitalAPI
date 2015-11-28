using System.Collections.Generic;
using CreateRandomDataTools.Interfaces.PrivateInterfaces;
using StorageModels.Models.HospitalModels;

namespace CreateRandomDataTools.DataCreators
{
    public class HospitalModelCreator : IHospitalModelCreator
    {
        public IEnumerable<HospitalStorageModel> GetList()
        {
            var firstHospitalProfile = new HospitalStorageModel
            {
                IsBlocked = false,
                Id = 0,
                Name = "Hospital 1",
                Address = "Address Hospital 1"
            };

            return new List<HospitalStorageModel>
            {
                firstHospitalProfile
            };
        }
    }
}

/*
 * Гродненская областная детская клиническая больница - ул. Островского, 22
 * Гродненская городская клиническая больница №1 - ул. Коммунальная, 2
 * Гродненский областной клинический перинатальный центр - ул. Горького, 77
 * Гродненский областной кардиологический центр - ул. Болдина, 9
 * Гродненская городская клиническая больница №2  - ул. Гагарина, 5
 * Гродненская городская клиническая больница №3 - б-р Ленинского Комсомола, 59
 * Гродненская городская клиническая больница №4  - пр-т Я. Купалы, 89
 * Гродненская городская клиническая больница №4 
 * Гродненская областная клиническая больница - б-р Ленинского Комсомола, 52
 * Гродненская городская больница скорой помощи - ул. Сов. Пограничников, 115
 * Военный госпиталь - ул. Дзержинского, 17
 * Гродненская городская поликлиника №1 - ул. Лермонтова, 13
 * Гродненская городская поликлиника №3 - ул. Пестрака, 4
*/