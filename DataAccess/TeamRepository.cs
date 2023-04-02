using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class TeamRepository : ITeamRepository
    {
        private readonly TournamentContext _tournamentContext;

        public TeamRepository(TournamentContext tournamentContext)
        {
            _tournamentContext = tournamentContext;
        }

        public async Task ResetTeamBoard()
        {
            var teams = await _tournamentContext.Team.ToListAsync();

            foreach (var team in teams)
            {
                team.Points = 0;
                team.Wins = 0;
                team.Losses = 0;
                team.Draws = 0;
                team.GoalFor = 0;
                team.GoalAgainst = 0;
                team.GoalDifference = 0;
            }
            await _tournamentContext.SaveChangesAsync();
        }

        public async Task UpdateTeamBoard(int matchId, int homeTeamScore, int awayTeamScore)
        {
            var match = await _tournamentContext.Match.FindAsync(matchId);

            var homeTeam = await _tournamentContext.Team.FindAsync(match.HomeTeamId);
            var awayTeam = await _tournamentContext.Team.FindAsync(match.AwayTeamId);

            if (homeTeamScore > awayTeamScore)
            {
                homeTeam.Points += 3;
                homeTeam.Wins += 1;
                awayTeam.Losses += 1;
            }
            else if (homeTeamScore < awayTeamScore)
            {
                awayTeam.Points += 3;
                awayTeam.Wins += 1;
                homeTeam.Losses += 1;
            }
            else
            {
                homeTeam.Points += 1;
                awayTeam.Points += 1;
                homeTeam.Draws += 1;
                awayTeam.Draws += 1;
            }

            homeTeam.GoalFor += homeTeamScore;
            homeTeam.GoalAgainst += awayTeamScore;
            homeTeam.GoalDifference = homeTeam.GoalFor - homeTeam.GoalAgainst;

            awayTeam.GoalFor += awayTeamScore;
            awayTeam.GoalAgainst += homeTeamScore;
            awayTeam.GoalDifference = awayTeam.GoalFor - awayTeam.GoalAgainst;

            match.HomeTeamScore = homeTeamScore;
            match.AwayTeamScore = awayTeamScore;
        }

        public async Task<List<Team>> GetStandings()
        {
            var teams = await _tournamentContext.Team.ToListAsync();

            // Takımları puanlarına, attığı gol fazlalığına ve yediği gol azlığına göre sıralar
            var orderedTeams = teams.OrderByDescending(t => t.Points)
                                    .ThenByDescending(t => t.GoalFor)
                                    .ThenBy(t => t.GoalAgainst);

            // Sıralanmış takımları döndürür
            return Enumerable.ToList(orderedTeams);
        }
    }
}