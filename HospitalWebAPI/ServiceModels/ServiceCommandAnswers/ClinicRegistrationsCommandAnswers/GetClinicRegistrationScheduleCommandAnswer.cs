﻿using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers
{
    public class GetClinicRegistrationScheduleCommandAnswer : AbstractMessagedCommandAnswer
    {
        public string Sex { get; set; }

        public int? SexId { get; set; }

        public string SectionProfile { get; set; }

        public int SectionProfileId { get; set; }

        public List<ClinicScheduleTableItem> Schedule { get; set; }

        public int CurrentHospitalId { get; set; }

        public List<KeyValuePair<int, string>> Hospitals { get; set; }
        public int? AgeCategoryId { get; set; }

        public List<KeyValuePair<int, string>> AgeCategories { get; set; }

        public bool HasGenderFactor { get; set; }
    }
}
