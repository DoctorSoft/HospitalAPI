using Enums.Enums;
using StorageModels.Interfaces;

namespace StorageModels.Models.ClinicModels
{
    public class PatientStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public string Code { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Years { get; set; }

        public int Months { get; set; }

        public int Weeks { get; set; }

        public Sex Sex { get; set; }

        public string PhoneNumber { get; set; }

        //

        public ReservationStorageModel Reservation { get; set; }

    }
}
