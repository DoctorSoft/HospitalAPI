using System.Collections.Generic;
using Enums.Enums;
using StorageModels.Interfaces;
using StorageModels.Models.ClinicModels;

namespace StorageModels.Models.HospitalModels
{
    public class EmptyPlaceByTypeStatisticStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public Sex? Sex { get; set; }

        public int Count { get; set; }

        //

        public EmptyPlaceStatisticStorageModel EmptyPlaceStatistic { get; set; }

        public ICollection<ReservationStorageModel> Reservations { get; set; }

        //

        public int EmptyPlaceStatisticId { get; set; }
    }
}
