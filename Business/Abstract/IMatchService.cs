using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Business.Abstract
{
    public interface IMatchService
    {
        Task<List<Match>> PlayTheMatchesOfTheWeek();
        Task<List<Match>> CreateFixture();
    }
}