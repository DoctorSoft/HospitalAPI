using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseModelConfigurations.Contexts;
using StorageModels.Models.ClinicModels;
using StorageModels.Models.HospitalModels;

namespace DataBaseChildClinicFiller
{
    class Program
    {
        public static void Main(string[] args)
        {
            var context = new TestDataBaseContext();

            //// Add Hospitals

            var hospitalsList = new List<HospitalStorageModel>
            {
                new HospitalStorageModel
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Гродненская областная детская клиническая больница",
                    Address = "г. Гродно, ул. Островского, 22"
                },
            };

            context.Set<HospitalStorageModel>().AddRange(hospitalsList);
            context.SaveChanges();

            //// Add Clinics

            var clinicsList = new List<ClinicStorageModel>
            {
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Гродненская детская поликлиника №1",
                    Address = "г. Гродно, ул. Даватора, 23"
                },
                new ClinicStorageModel
                {
                    IsBlocked = false,
                    Id = 0,
                    Name = "Гродненская детская поликлиника №2",
                    Address = "г. Гродно, ул. Гагарина 18"
                },
            };
            context.Set<ClinicStorageModel>().AddRange(clinicsList);
            context.SaveChanges();


        }
    }
}
