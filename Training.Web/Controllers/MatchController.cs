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

        [HttpGet]
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

        //[HttpPost]
        //public ActionResult Index(MatchIndexViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var match = new Match
        //    {
        //        HomeTeamId = model.SelectedHomeTeamId,
        //        HomeScore = model.HomeTeamScore.GetValueOrDefault(),
        //        AwayTeamId = model.SelectedAwayTeamId,
        //        AwayScore = model.AwayTeamScore.GetValueOrDefault(),
        //        LeagueId = model.LeagueId,
        //        MatchDateTime = DateTime.Now
        //    };

        //    service.InsertMatch(match);

        //    return RedirectToAction("Index", new { id = model.LeagueId });
        //}
    }
}
