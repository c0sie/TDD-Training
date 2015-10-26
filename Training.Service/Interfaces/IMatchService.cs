using System.Collections.Generic;
using Training.Entities.Models;

namespace Training.Service.Interfaces
{
    public interface IMatchService
    {
        IEnumerable<Team> GetTeamsByLeague(int id);
    }
}
