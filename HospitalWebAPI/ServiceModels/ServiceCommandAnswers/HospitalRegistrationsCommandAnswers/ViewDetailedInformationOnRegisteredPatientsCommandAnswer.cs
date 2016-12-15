using System.Collections.Generic;
using ServiceModels.ModelTools;
using ServiceModels.ServiceCommandAnswers.ClinicRegistrationsCommandAnswers.Entities;

namespace ServiceModels.ServiceCommandAnswers.HospitalRegistrationsCommandAnswers
{
    public class ViewDetailedInformationOnRegisteredPatientsCommandAnswer : AbstractMessagedCommandAnswer
    {
        public int EmptyPlaceByTypeStatisticId { get; set; }
        public int? FullInformation { get; set; }
        public int HospitalProfileId { get; set; }
        public List<ClinicBreakRegistrationTableItem> Table { get; set; }
        public string Date { get; set; }
        public bool ShowModalWindow { get; set; }
    }
}
