using System;
using ServiceModels.ModelTools;

namespace ServiceModels.ServiceCommands.HospitalRegistrationsCommands
{
    public class ViewDetailedInformationOnRegisteredPatientsCommand : AbstractTokenCommand
    {
        public int HospitalProfileId { get; set; }
        public int EmptyPlaceByTypeStatisticId { get; set; }
        public int? FullInformation { get; set; }
        public string Date { get; set; }
        public bool ShowModalWindow { get; set; }
    }
}
