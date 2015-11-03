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
        }

        public ActionResult Index(int id)
        {
            var model = new MatchIndexViewModel();

            try
            {
                model.Teams = service.GetTeamsByLeague(id);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            model.LeagueId = id;

            return View(model);
        }
    }
}
