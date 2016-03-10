using Enums.Enums;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities
{
    public class HospitalRegistrationCountStatisticItem
    {
        public int Id { get; set; }

        public Sex? Sex { get; set; }
        
        public int OpenCount { get; set; }

        public int RegisteredCount { get; set; }
    }
}
