using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Business.Abstract
{
    public interface ITeamService
    {
        Task<List<Team>> GetStandings();
        
        Task ResetTeamBoard();
    }
}