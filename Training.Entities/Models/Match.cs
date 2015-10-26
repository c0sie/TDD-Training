using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Training.Entities.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("HomeTeam")]
        public int HomeTeamId { get; set; }

        [Required]
        [ForeignKey("AwayTeam")]
        public int AwayTeamId { get; set; }

        [Required]
        public int HomeScore { get; set; }

        [Required]
        public int AwayScore { get; set; }

        [Required]
        [ForeignKey("League")]
        public int LeagueId { get; set; }

        [Required]
        public DateTime MatchDateTime { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Team HomeTeam { get; set; }

        public virtual Team AwayTeam { get; set; }

        public virtual League League { get; set; }
    }
}
