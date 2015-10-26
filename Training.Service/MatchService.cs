using System;
using System.Collections.Generic;
using System.Linq;
using Training.Entities.Models;
using Training.Repository.Pattern.Interfaces;
using Training.Service.Exceptions;
using Training.Service.Interfaces;

namespace Training.Service
{
    public class MatchService : IMatchService
    {
        private readonly IUnitOfWork unitOfWork;

        public MatchService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Team> GetTeamsByLeague(int id)
        {
            // Get list of all active leagues.
            var leagues = unitOfWork.Repository<League>().Query(x => !x.IsDeleted && x.Id == id).Select();

            // If all the league Id's do not match the id passed in then the id is invalid - throw exception.
            if (leagues.All(x => x.Id != id))
            {
                throw new ArgumentException("Invalid leagueId", id.ToString());
            }

            var teams = unitOfWork.Repository<Team>().Query(x => x.LeagueId == id).Select();

            return teams;
        }

        public Match InsertMatch(Match match)
        {
            var existingMatch = unitOfWork.Repository<Match>()
                .Query(x =>
                        x.HomeTeamId == match.HomeTeamId && x.AwayTeamId == match.AwayTeamId &&
                        x.MatchDateTime == match.MatchDateTime)
                .Select().ToList()
                .FirstOrDefault();

            if (existingMatch != null)
            {
                throw new DuplicateMatchException(existingMatch.Id);
            }

            unitOfWork.Repository<Match>().Insert(match);
            unitOfWork.SaveChanges();

            return match;
        }
    }
}
