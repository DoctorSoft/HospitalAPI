using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers
{
    public class GetClinicUserMainPageInformationCommandAnswer : AbstractTokenCommandAnswer
    {
        public string UserName { get; set; }
        public bool? ReservationStatus { get; set; }
        public string StartTimeReservation { get; set; }
        public string EndTimeReservation { get; set; }
        public int? CountNewNotices { get; set; }
    }
}
