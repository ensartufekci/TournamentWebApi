using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess;
using DataAccess.Models;

namespace Business
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;

        public MatchService(IMatchRepository matchRepository, ITeamRepository teamRepository)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
        }

        public async Task<List<Match>> CreateFixture()
        {
            await _matchRepository.ClearFixture();
            await _teamRepository.ResetTeamBoard();
            List<Match> matches = await _matchRepository.CreateFixture();

            return matches;
        }
        
        
        public async Task<List<Match>> PlayTheMatchesOfTheWeek()
        {
            var playTheMatchesOfTheWeek = await _matchRepository.PlayTheMatchesOfTheWeek();
            return playTheMatchesOfTheWeek;
        }
    }
}