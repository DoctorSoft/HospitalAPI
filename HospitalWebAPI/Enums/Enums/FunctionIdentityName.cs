namespace Enums.Enums
{
    public enum FunctionIdentityName
    {
        FillHospitalEmptyPlaces = 0,
        WatchEmptyPalacesByHospital = 1,
        GetHospitalWarningNotices = 2,
        EditEmptyPlacesByHospital = 3,
        BreakEmptyPlaceReservationsByHospital = 4,
        WatchEmptyPlaceReservationByHospital = 5,


        WatchEmptyPlacesList = 100,
        MakeEmptyPlaceReservation = 101,
        BreakEmptyPlaceReservationByClinic = 102,
        WatchEmptyPlaceReservationsByClinic = 103,
        GetClinicWarningNotices = 104,


        SendAutoWarningNotices = 200,

        // 300+ numbers are for administrator functions only (By default admin can do everything)

        WatchRegistrationStatistic = 400
    }
}
