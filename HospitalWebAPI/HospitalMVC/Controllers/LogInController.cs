﻿using System;
using System.Linq;
using System.Web.Mvc;
using ServiceModels.ServiceCommands.AuthorizationCommands;
using Services.Interfaces.AuthorizationServices;

namespace HospitalMVC.Controllers
{
    public class LogInController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        private const string RedirectControllerName = "LogInRedirect";
        private const string RedirectActionName = "RedirectToMainPage";

        public LogInController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        // GET: LogIn
        public ActionResult Index(GetTokenByUserCredentialsCommand command)
        {
            return View(command);
        }

        [HttpPost]
        public ActionResult Authorize(GetTokenByUserCredentialsCommand command)
        {
            var model = _authorizationService.GetTokenByUserCredentials(command);

            if (!model.Errors.Any())
            {
                return RedirectToAction(RedirectActionName, RedirectControllerName, new { Token = model.Token });
            }
            
            foreach (var error in model.Errors)
            {
                ModelState.AddModelError(error.FieldName, error.Title);
            }

            return View("Index", command);
        }
    }
}