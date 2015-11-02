using System;
using System.Web.Mvc;
using Training.Service.Interfaces;
using Training.Web.Models;

namespace Training.Web.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService service;

        public MatchController(IMatchService service)
        {
            this.service = service;
            // TODO: Time to learn DI!!
        }

        public ActionResult Index(int leagueId = 1)
        {
            var model = new MatchIndexViewModel();

            try
            {
                model.Teams = service.GetTeamsByLeague(leagueId);
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError(string.Empty, "The league you have chosen in invalid, please select another league.");
            }

            return View(model);
        }
    }
}