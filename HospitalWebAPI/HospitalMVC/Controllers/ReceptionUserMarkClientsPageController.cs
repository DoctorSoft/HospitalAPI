using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.ReceptionMarkingCommands;
using Services.Interfaces.ReceptionMarkingServices;

namespace HospitalMVC.Controllers
{
    public class ReceptionUserMarkClientsPageController : Controller
    {
        private readonly IReceptionMarkingService _receptionMarkingService;

        public ReceptionUserMarkClientsPageController(IReceptionMarkingService receptionMarkingService)
        {
            _receptionMarkingService = receptionMarkingService;
        }

        [TokenAuthorizationFilter(FunctionIdentityName.ReceptionUserPrimaryAccess, FunctionIdentityName.ReceptionUserMarkClients)]
        public ActionResult Index(GetReceptionUserMarkClientsPageInformationCommand command)
        {
            var answer = _receptionMarkingService.GetReceptionUserMarkClientsPageInformation(command);
            return View(answer);
        }
    }
}