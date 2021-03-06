﻿using System.Linq;
using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommandAnswers.NoticesCommandAnswers;
using ServiceModels.ServiceCommands.NoticesCommands;
using Services.Interfaces.NoticesService;

namespace HospitalMVC.Controllers
{
    public class HospitalUserSendDistributiveMessagesPageController : Controller
    {
        private readonly INoticesService _noticesService;

        public HospitalUserSendDistributiveMessagesPageController(INoticesService noticesService)
        {
            _noticesService = noticesService;
        }

        // GET: HospitalUserSendDistributiveMessagesPage
        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserSendDistributionMessages)]
        public ActionResult Index(GetSendDistributiveMessagesPageInformationCommand command)
        {
            return View(new GetHospitalNoticesMessageInformationCommandAnswer
            {
                Token = command.Token.Value,
                Text = command.Text,
                Title = command.Title
            });
        }

        [HttpPost]
        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserSendDistributionMessages)]
        public ActionResult SendMessages(GetSendDistributiveMessagesPageInformationCommand command)
        {
            var answer = _noticesService.GetSendDistributiveMessagesPageInformation(command);

            if (answer.Errors.Any())
            {
                var model = new GetHospitalNoticesMessageInformationCommandAnswer
                {
                    Token = answer.Token,
                    Title = answer.Title,
                    Text = answer.Text,
                    Errors = answer.Errors
                };
                ViewBag.Errors = answer.Errors;
                return View("Index", model);
            }

            return RedirectToAction("Index", "Home", new GetHospitalNoticesMessageInformationCommandAnswer
            {
                Token = command.Token.Value,
                Text = command.Text,
                Title = command.Title
            });
        }
    }
}