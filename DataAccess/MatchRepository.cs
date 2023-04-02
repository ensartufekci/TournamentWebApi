using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MatchRepository : IMatchRepository
    {
        private readonly TournamentContext _tournamentContext;
        private readonly ITeamRepository _teamRepository;

        public MatchRepository(TournamentContext tournamentContext, ITeamRepository teamRepository)
        {
            _tournamentContext = tournamentContext;
            _teamRepository = teamRepository;
        }

        public async Task<List<Match>> CreateFixture()
        {
            try
            {
                var teams =await _tournamentContext.Team.ToListAsync();
            
                var matches = new List<Match>();
                var random = new Random();
                for (int week = 1; week <= 7; week++)
                {
                    var remainingTeams = new List<Team>(teams);

                    while (remainingTeams.Count > 1)
                    {
                        var homeTeamIndex = random.Next(remainingTeams.Count);
                        var homeTeam = remainingTeams[homeTeamIndex];
                        remainingTeams.RemoveAt(homeTeamIndex);

                        var awayTeamIndex = random.Next(remainingTeams.Count);
                        var awayTeam = remainingTeams[awayTeamIndex];
                        remainingTeams.RemoveAt(awayTeamIndex);

                        var match = new Match
                        {
                            HomeTeamId = homeTeam.Id,
                            AwayTeamId = awayTeam.Id,
                            HomeTeamScore = 0,
                            AwayTeamScore = 0,
                            Week = week,
                            IsPlayed = false
                        };
                        matches.Add(match);
                    }
                }
            
                await _tournamentContext.Match.AddRangeAsync(matches);

                await _tournamentContext.SaveChangesAsync();
                
                return matches;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public async Task ClearFixture()
        {
            var matches =await _tournamentContext.Match.ToListAsync();

            if (matches != null && matches.Count>0)
            {
                _tournamentContext.Match.RemoveRange(matches);
            }
            
        }

        public async Task<List<Match>> PlayTheMatchesOfTheWeek()
        {
            var nextWeek = await _tournamentContext.Match.OrderBy(x=>x.Week).FirstOrDefaultAsync(x=>x.IsPlayed==false);

            try
            {
                var matches = await _tournamentContext.Match.Where(x => x.Week == nextWeek.Week).ToListAsync();

                var random = new Random();
                foreach (var match in matches)
                {
                    match.HomeTeamScore = random.Next(0, 8);
                    match.AwayTeamScore = random.Next(0, 8);
                    match.IsPlayed = true;

                    await _teamRepository.UpdateTeamBoard(match.Id, match.HomeTeamScore, match.AwayTeamScore);
                }

                await _tournamentContext.SaveChangesAsync();
                
                return matches;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Oynanacak Maç Kalmadı..");
            }

            
        }
    }
    
}