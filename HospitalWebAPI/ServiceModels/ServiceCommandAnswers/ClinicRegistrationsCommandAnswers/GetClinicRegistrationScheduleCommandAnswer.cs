using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers
{
    public class GetClinicRegistrationScheduleCommandAnswer : AbstractTokenCommandAnswer
    {
        public int Sex { get; set; }

        public int AgeSection { get; set; }

        public int SectionProfileId { get; set; }

        // Some schedule data
    }
}
