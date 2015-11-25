﻿using System;
using StorageModels.Enums;
using StorageModels.Interfaces;
using StorageModels.Models.HospitalModels;

namespace StorageModels.Models.ClinicModels
{
    public class ReservationStorageModel : IIdModel
    {
        public int Id { get; set; }

        //

        public ReservationStatus Status { get; set; }
        public DateTime ApproveTime { get; set; }
        public DateTime? CancelTime { get; set; }

        //

        public EmptyPlaceStatisticStorageModel EmptyPlaceStatistic { get; set; }
        public ClinicStorageModel Clinic { get; set; }
        public PatientStorageModel Patient { get; set; }
    }
}