﻿using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers
{
    public class GetClinicRegistrationScheduleCommandAnswer : AbstractTokenCommandAnswer
    {
        public int Sex { get; set; }

        public int AgeSection { get; set; }

        public int SectionProfileId { get; set; }

        public List<ClinicScheduleTableItem> Schedule { get; set; }

        public int CurrentHospitalId { get; set; }

        public List<KeyValuePair<int, string>> Hospitals { get; set; }
    }
}
