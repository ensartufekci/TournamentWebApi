using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess
{
    public interface IMatchRepository
    {
        Task<List<Match>> CreateFixture();
        Task ClearFixture();
        Task<List<Match>> PlayTheMatchesOfTheWeek();
    }
}