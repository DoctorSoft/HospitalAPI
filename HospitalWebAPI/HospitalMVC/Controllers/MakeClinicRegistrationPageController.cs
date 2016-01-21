﻿using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.ClinicRegistrationsCommands;
using Services.Interfaces.ClinicRegistrationsServices;

namespace HospitalMVC.Controllers
{
    public class MakeClinicRegistrationPageController : Controller
    {
        private readonly IClinicRegistrationsService _clinicRegistrationsService;

        public MakeClinicRegistrationPageController(IClinicRegistrationsService clinicRegistrationsService)
        {
            _clinicRegistrationsService = clinicRegistrationsService;
        }

        // GET: MakeClinicRegistrationPage
        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess, FunctionIdentityName.ClinicUserMakeRegistrations)]
        public ActionResult Index(GetMakeClinicRegistrationsPageInformationCommand command)
        {
            var answer = _clinicRegistrationsService.GetMakeClinicRegistrationsPageInformation(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess, FunctionIdentityName.ClinicUserMakeRegistrations)]
        public ActionResult Step2(GetClinicRegistrationScheduleCommand command)
        {
            var answer = _clinicRegistrationsService.GetClinicRegistrationSchedule(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.ClinicUserPrimaryAccess, FunctionIdentityName.ClinicUserMakeRegistrations)]
        public ActionResult Step3(GetClinicRegistrationUserFormCommand command)
        {
            var answer = _clinicRegistrationsService.GetClinicRegistrationUserForm(command);
            return View(answer);
        }
    }
}