using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public int LeagueId { get; set; }

        [Display(Name = "Home Team")]
        [Required]
        public int SelectedHomeTeamId { get; set; }

        [Display(Name = "Score")]
        [Required]
        public int? HomeTeamScore { get; set; }

        [Display(Name = "Away Team")]
        [Required]
        public int SelectedAwayTeamId { get; set; }

        [Display(Name = "Score")]
        [Required]
        public int? AwayTeamScore { get; set; }
    }
}
