using System;
using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class ViewRegistrationDetailsMaleCommandAnswer : AbstractTokenCommandAnswer
    {
        public int HospitalProfileId { get; set; }
        public List<ClinicBreakRegistrationTableItem> Table { get; set; }
        public DateTime Date { get; set; }
    }
}
