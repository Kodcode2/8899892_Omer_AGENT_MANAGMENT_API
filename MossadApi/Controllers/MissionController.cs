using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadApi.DAL;
using MossadApi.Models;
namespace MossadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly ILogger<MissionController> _logger;

        public MissionController(ILogger<MissionController> logger, DBContext context)
        {

            this._context = context;
            this._logger = logger;
        }


        [HttpPost("update")]
        public async Task<IActionResult> updatmission()
        {
            //לוגיקה
            return Ok(200);
        }


        [HttpGet]
        public async Task<IActionResult> getmissions()
        {
           var missions = await _context.Mission.ToListAsync();
            return StatusCode(StatusCodes.Status200OK,  missions);
        }

        [HttpPut("{id}")]
        public async Task <IActionResult> updatestatus(int id)
        {
          Mission mission = await _context.Mission.FindAsync(id);
            if (mission == null) {
                return BadRequest(400);
            }
            mission.Status = "assigned";
            //await _context.Mission.SingleAsync();
            await _context.SaveChangesAsync();
            return Ok();
        }

       

    }

    
}
