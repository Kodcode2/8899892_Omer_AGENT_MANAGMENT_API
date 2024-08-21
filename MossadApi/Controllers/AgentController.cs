using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadApi.DAL;

using Microsoft.EntityFrameworkCore;
using MossadApi.Models;

namespace MossadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly ILogger<AgentController> _logger;

        public AgentController(ILogger<AgentController> logger, DBContext context)
        {

            this._context = context;
            this._logger = logger;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAgent([FromBody] Agents agent)
        {
            this._context.Agents.Add(agent);
            return StatusCode
                (StatusCodes.Status201Created, new { Response = true, agent = agent });
        }

        [HttpGet]
        public async Task<IActionResult> getagents()
        {
            var attacks = await this._context.attacks.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, attacks);
        }



    }

    


}
