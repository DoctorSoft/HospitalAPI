using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.ClinicRegistrationsCommands
{
    public class SaveClinicRegistrationCommand : AbstractTokenCommand
    {
        public int SexId { get; set; }

        public int AgeSectionId { get; set; }

        public int SectionProfileId { get; set; }

        public int CurrentHospitalId { get; set; }

        public string Sex { get; set; }

        public string AgeSection { get; set; }

        public string SectionProfile { get; set; }

        public string CurrentHospital { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }

        public string Code { get; set; }

        public string PhoneNumber { get; set; }
    }
}
