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
        private readonly Icalculatlocation _icalculatlocation;

        public MissionController(ILogger<MissionController> logger, DBContext context, Icalculatlocation icalculatlocation)
        {

            this._context = context;
            this._logger = logger;
            _icalculatlocation = icalculatlocation;
        }


        //[HttpPost("update")]
        //public async Task<IActionResult> updatmission()
        //{
        //    List<Mission> assigned = await _context.Mission
        //    .Where(m => m.Status == "assigned")
        //    .ToListAsync();
        //    foreach (Mission mission in assigned)
        //    {
        //        Agents agents = await _context.Agents.FindAsync(mission.Id);
        //        Target target = await _context.Targets.FindAsync(mission.Id);
        //        Dictionary<string, string> dict = await _icalculatlocation.directioncalculation(target, agents);
        //        if (dict["direction"] == "touchdown")
        //        {
        //            mission.Status = "finish";
        //        }
        //        else 
        //        {
        //            await _icalculatlocation.AgentLocation(agents, dict);
        //            mission.Timelaft = await _icalculatlocation.timetotarget(target, agents);
        //        }
        //    }

        //    await _context.SaveChangesAsync();

        //    return Ok(200);
        //}

        [HttpPost("update")]
        public async Task<IActionResult> updatmission()
        {
            List<Mission> assigned = await _context.Mission
                .Where(m => m.Status == "assigned")
                .ToListAsync();

            var tasks = assigned.Select(async mission =>
            {
                Agents agents = await _context.Agents.FindAsync(mission.Id);
                Target target = await _context.Targets.FindAsync(mission.Id);
                Dictionary<string, string> dict = await _icalculatlocation.directioncalculation(target, agents);

                if (dict["direction"] == "touchdown")
                {
                    mission.Status = "finish";
                }
                else
                {
                    await _icalculatlocation.AgentLocation(agents, dict);
                    mission.Timelaft = await _icalculatlocation.timetotarget(target, agents);
                }
            }).ToList();

            await Task.WhenAll(tasks);
            await _context.SaveChangesAsync();
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
            await _context.SaveChangesAsync();
            return Ok();
        }

       

    }

    
}
