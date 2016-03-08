using ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers;
using ServiceModels.ServiceCommands.NoticesCommands;

namespace Services.Interfaces.NoticesService
{
    public interface INoticesService
    {
        GetClinicNoticesPageInformationCommandAnswer GetClinicNoticesPageInformation(
            GetClinicNoticesPageInformationCommand command);

        GetHospitalNoticesPageInformationCommandAnswer GetHospitalNoticesPageInformation(
            GetHospitalNoticesPageInformationCommand command);

        GetSendDistributiveMessagesPageInformationCommandAnswer GetSendDistributiveMessagesPageInformation(
            GetSendDistributiveMessagesPageInformationCommand command);

        GetClinicMessageByIdCommandAnswer GetClinicMessageById(GetClinicMessageByIdCommand command);

        RemoveClinicMessageByIdCommandAnswer RemoveClinicMessageById(RemoveClinicMessageByIdCommand command);

        GetHospitalMessageByIdCommandAnswer GetHospitalMessageById(GetHospitalMessageByIdCommand command);

        RemoveHospitalMessageByIdCommandAnswer RemoveHospitalMessageById(RemoveHospitalMessageByIdCommand command);

        ShowDischargesListCommandAnswer ShowDischargesList(ShowDischargesListCommand command);

        ShowPageToSendDischangeCommandAnswer ShowPageToSendDischange(ShowPageToSendDischangeCommand command);

        SaveDischargeCommandAnswer SaveDischarge(SaveDischargeCommand command);

        DownloadDischargeCommandAnswer DownloadDischarge(DownloadDischargeCommand command);
    }
}
