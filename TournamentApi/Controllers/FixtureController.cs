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
    public class FixtureController : ControllerBase
    {

        private readonly IMatchService _matchService;

        public FixtureController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<List<Match>>> Create()
        {
            var matches = await _matchService.CreateFixture();
            return Ok(matches);
        }
        
        [HttpPost("PlayTheMatchesOfTheWeek")]
        public async Task<ActionResult<List<Match>>> PlayTheMatchesOfTheWeek()
        {
            try
            {
                var playTheMatchesOfTheWeek = await _matchService.PlayTheMatchesOfTheWeek();
                return Ok(playTheMatchesOfTheWeek);
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}