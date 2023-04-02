using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess
{
    public interface ITeamRepository
    {
        Task UpdateTeamBoard(int matchId, int homeTeamScore, int awayTeamScore);

        Task ResetTeamBoard();

        Task<List<Team>> GetStandings();

    }
}