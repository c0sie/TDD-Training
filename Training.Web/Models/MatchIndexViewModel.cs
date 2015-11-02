using System.Collections.Generic;
using Training.Entities.Models;

namespace Training.Web.Models
{
    public class MatchIndexViewModel
    {
        public MatchIndexViewModel()
        {
            Teams = new List<Team>();
        }

        public IEnumerable<Team> Teams { get; set; }

        public int? SelectedTeamId { get; set; }

        public Match match { get; set; }
    }
}