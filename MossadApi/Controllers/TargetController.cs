using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadApi.DAL;
using MossadApi.Models;
namespace MossadApi.Controllers
{
    [Route("/[controller]s")]
    [ApiController]
    public class targetController : ControllerBase
    {
        
        private readonly Icalculatlocation _icalculatlocation;
        private readonly ISetmission _setmission;
       // private readonly SetMission setMission;
        private readonly DBContext _context;
        private readonly ILogger<targetController> _logger;

        public targetController(ILogger<targetController> logger, DBContext context, Icalculatlocation icalculatlocation, ISetmission setmission)
        {

            this._context = context;
            this._logger = logger;
            this._icalculatlocation = icalculatlocation;
            this._setmission = setmission;
           
        }

        [HttpPut("{id}/pin")]
        public async Task<IActionResult> putlocation(int id, Dictionary<string, int> location)
        {
           Target target = await _context.Targets.FindAsync(id);
            if (target == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            target.X_axis = location["x"];
            target.Y_axis = location["y"];
            await this._context.SaveChangesAsync();
            return StatusCode(200);

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
                (StatusCodes.Status201Created, target );
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
