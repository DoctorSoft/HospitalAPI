using System.Web.Mvc;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.NoticesCommands;
using Services.Interfaces.NoticesService;

namespace HospitalMVC.Controllers
{
    public class ClinicNoticesPageController : Controller
    {
        private readonly INoticesService _noticesService;

        public ClinicNoticesPageController(INoticesService noticesService)
        {
            _noticesService = noticesService;
        }

        // GET: ClinicNoticesPage
        [TokenAuthorizationFilter]
        public ActionResult Index(GetClinicNoticesPageInformationCommand command)
        {
            var answer = _noticesService.GetClinicNoticesPageInformation(command);
            return View(answer);
        }
    }
}