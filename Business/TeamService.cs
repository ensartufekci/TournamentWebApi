using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess;
using DataAccess.Models;

namespace Business
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<List<Team>> GetStandings()
        {
            return await _teamRepository.GetStandings();
        }

        public async Task ResetTeamBoard()
        {
            await _teamRepository.ResetTeamBoard();
        }
    }
}