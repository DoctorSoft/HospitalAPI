using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.MainPageCommandAnswers
{
    public class GetClinicUserMainPageInformationCommandAnswer : AbstractTokenCommandAnswer
    {
        public string UserName { get; set; }
        public bool? ReservationStatus { get; set; }
        public TimeSpan StartTimeReservation { get; set; }
        public TimeSpan EndTimeReservation { get; set; }
        public int? CountNewNotices { get; set; }
    }
}
