using System.Web.Mvc;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.HospitalRegistrationsCommands;
using Services.Interfaces.HospitalRegistrationsService;

namespace HospitalMVC.Controllers
{
    public class HospitalRegistrationsPageController : Controller
    {
        private readonly IHospitalRegistrationsService _hospitalRegistrationsService;

        public HospitalRegistrationsPageController(IHospitalRegistrationsService hospitalRegistrationsService)
        {
            _hospitalRegistrationsService = hospitalRegistrationsService;
        }

        // GET: HospitalRegistrationsPage
        [TokenAuthorizationFilter]
        public ActionResult Index(GetOpenHospitalRegistrationsPageInformationCommand command)
        {
            var answer = _hospitalRegistrationsService.GetOpenHospitalRegistrationsPageInformation(command);
            return View(answer);
        }
    }
}