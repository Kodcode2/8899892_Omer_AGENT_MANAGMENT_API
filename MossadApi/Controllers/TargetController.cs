using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadApi.DAL;
using MossadApi.Models;
namespace MossadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        
        private readonly Icalculatlocation _icalculatlocation;
        private readonly ISetmission _setmission;
       // private readonly SetMission setMission;
        private readonly DBContext _context;
        private readonly ILogger<TargetController> _logger;

        public TargetController(ILogger<TargetController> logger, DBContext context, Icalculatlocation icalculatlocation, ISetmission setmission)
        {

            this._context = context;
            this._logger = logger;
            this._icalculatlocation = icalculatlocation;
            this._setmission = setmission;
           
        }


        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> createtarget([FromBody] Target target)
        {
            this._context.Targets.Add(target);
            await this._context.SaveChangesAsync();

            _setmission.Set();
            return StatusCode
                (StatusCodes.Status201Created, new { Response = true, target = target });
        }

        [HttpGet]
        public async Task<IActionResult> gettargets()
        {
            List<Target> targets = await _context.Targets.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, targets);
        }

        //שרת סימולציה בלבד
        [HttpPut("{id}/move")]
        public async Task<IActionResult> updatlocation(int id, [FromBody] Dictionary<string, string> move)
        {
            Target target = await _context.Targets.FindAsync(id);
            if (target == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            target = _icalculatlocation.TargetLocation(target, move);
            
            return StatusCode(200, new { target = target });

        }
    }
}
