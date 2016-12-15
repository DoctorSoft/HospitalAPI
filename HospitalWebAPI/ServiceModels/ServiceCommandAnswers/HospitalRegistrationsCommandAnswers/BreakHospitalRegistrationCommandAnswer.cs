using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class BreakHospitalRegistrationCommandAnswer : AbstractMessagedCommandAnswer
    {
        public int HospitalProfileId { get; set; }
        public int EmptyPlaceByTypeStatisticId { get; set; }
        public int? FullInformation { get; set; }
        public string Date { get; set; }

    }
}
