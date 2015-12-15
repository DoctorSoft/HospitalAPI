﻿using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.NoticesCommands;
using Services.Interfaces.NoticesService;

namespace HospitalMVC.Controllers
{
    public class HospitalNoticesPageController : Controller
    {
        private readonly INoticesService _noticesService;

        public HospitalNoticesPageController(INoticesService noticesService)
        {
            _noticesService = noticesService;
        }

        // GET: HospitalNoticesPage
        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserShowMessages)]
        public ActionResult Index(GetHospitalNoticesPageInformationCommand command)
        {
            var answer = _noticesService.GetHospitalNoticesPageInformation(command);
            return View(answer);
        }
    }
}