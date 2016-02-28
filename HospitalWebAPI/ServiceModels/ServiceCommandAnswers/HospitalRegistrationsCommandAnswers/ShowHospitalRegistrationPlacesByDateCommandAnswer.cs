using System;
using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class ShowHospitalRegistrationPlacesByDateCommandAnswer : AbstractTokenCommandAnswer
    {
        public List<HospitalRegistrationTableItem> Table { get; set; }

        public DateTime Date { get; set; }

        public bool IsCompleted { get; set; }

        public bool ShowModalWindow { get; set; }
    }
}
