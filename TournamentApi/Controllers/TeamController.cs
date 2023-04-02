using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business;
using Business.Abstract;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace TournamentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet("Standings")]
        public async Task<ActionResult<List<Match>>> Standings()
        {
            var stand = await _teamService.GetStandings();

            return Ok(stand);
        }
        
    }
}