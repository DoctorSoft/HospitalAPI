using Enums.Enums;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities
{
    public class HospitalRegistrationCountStatisticItem
    {
        public Sex Sex { get; set; }

        public AgeSection AgeSection { get; set; }

        public int OpenCount { get; set; }
    }
}
