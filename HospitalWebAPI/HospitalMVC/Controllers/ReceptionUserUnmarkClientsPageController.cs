using System.Web.Mvc;
using Enums.Enums;
using HospitalMVC.Filters;
using ServiceModels.ServiceCommands.ReceptionMarkingCommands;
using Services.Interfaces.ReceptionMarkingServices;

namespace HospitalMVC.Controllers
{
    public class ReceptionUserUnmarkClientsPageController : Controller
    {
        private readonly IReceptionMarkingService _receptionMarkingService;

        public ReceptionUserUnmarkClientsPageController(IReceptionMarkingService receptionMarkingService)
        {
            _receptionMarkingService = receptionMarkingService;
        }

        [TokenAuthorizationFilter(FunctionIdentityName.ReceptionUserPrimaryAccess, FunctionIdentityName.ReceptionUserMarkClients, FunctionIdentityName.ReceptionUserUnmarkClients)]
        public ActionResult Index(GetReceptionUserUnmarkClientsPageInformationCommand command)
        {
            var answer = _receptionMarkingService.GetReceptionUserUnmarkClientsPageInformation(command);
            return View(answer);
        }

        [TokenAuthorizationFilter(FunctionIdentityName.ReceptionUserPrimaryAccess, FunctionIdentityName.ReceptionUserMarkClients)]
        public ActionResult MarkClientAsArriving(MarkClientAsArrivingCommand command)
        {
            var answer = _receptionMarkingService.MarkClientAsArriving(command);
            return RedirectToAction("Index", new { Token = answer.Token });
        }
    }
}