using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.HospitalRegistrationsCommands;
using Services.Interfaces.HospitalRegistrationsService;

namespace HospitalMVC.Controllers
{
    public class ChangeHospitalRegistrationPageController : Controller
    {
        private readonly IHospitalRegistrationsService _hospitalRegistrationsService;

        public ChangeHospitalRegistrationPageController(IHospitalRegistrationsService hospitalRegistrationsService)
        {
            _hospitalRegistrationsService = hospitalRegistrationsService;
        }

        // GET: ChangeHospitalRegistrationPage
        [TokenAuthorizationFilter(FunctionIdentityName.HospitalUserPrimaryAccess, FunctionIdentityName.HospitalUserChangeEmptyPlaces)]
        public ActionResult Index(GetChangeHospitalRegistrationsPageInformationCommand command)
        {
            var answer = _hospitalRegistrationsService.GetChangeHospitalRegistrationsPageInformation(command);
            return View(answer);
        }
    }
}