using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadApi.DAL;
using MossadApi.Models;
namespace MossadApi.Controllers
{
    [Route("[controller]s")]
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


        [HttpPost("update")]
        public async Task<IActionResult> updatmission()
        {
            List<Mission> assigned = await _context.Mission
            .Where(m => m.Status == "assigned")
            .ToListAsync();
            if (assigned != null)
            {
                foreach (Mission mission in assigned)
                {
                    Agents agents = await _context.Agents.FindAsync(mission.AgentId);
                    Target target = await _context.Targets.FindAsync(mission.TargetId);
                    agents = await _icalculatlocation.movment(target,agents);
                    Dictionary<string, string> dict = await _icalculatlocation.directioncalculation(target, agents);
                    if (agents.X_axis == target.X_axis && agents.Y_axis == target.Y_axis)
                    {
                        mission.Status = "finish";
                        agents.Active = false;
                        target.Alive = false;
                    }
                    else
                    {
                        mission.Timelaft = await _icalculatlocation.timetotarget(target, agents);
                    }
                }
            }
            await _context.SaveChangesAsync();

            return Ok(200);
        }

        //[HttpPost("update")]
        //public async Task<IActionResult> updatmission()
        //{
        //    List<Mission> assigned = await _context.Mission
        //        .Where(m => m.Status == "assigned")
        //        .ToListAsync();
        //    if (assigned != null)
        //    {
        //        var tasks = assigned.Select(async mission =>
        //        {
        //            Agents agents = await _context.Agents.FindAsync(mission.AgentId);
        //            Target target = await _context.Targets.FindAsync(mission.TargetId);
        //            Dictionary<string, string> dict = await _icalculatlocation.directioncalculation(target, agents);

        //            if (dict["direction"] == "touchdown")
        //            {
        //                mission.Status = "finish";
        //                agents.Active = false;
        //                target.Alive = false;
        //            }
        //            else
        //            {
        //                await _icalculatlocation.AgentLocation(agents, dict);
        //                mission.Timelaft = await _icalculatlocation.timetotarget(target, agents);
        //            }
        //        }).ToList();

        //       //await Task.WhenAll(tasks);

        //        await _context.SaveChangesAsync();
        //        return Ok(200);
        //    }
        //    else { return BadRequest(); }
        //}




        [HttpGet]
        public async Task<IActionResult> getmissions()
        {
           var missions = await _context.Mission.
                ToListAsync();
            return StatusCode(StatusCodes.Status200OK,  missions);
        }





        [HttpPut("{id}")]
        public async Task <IActionResult> updatestatus(int id)
        {
          Mission mission = await _context.Mission.FindAsync(id);
            if (mission == null) {
                return NotFound();
            }
            Agents agents = await _context.Agents.FindAsync(mission.AgentId);
            agents.Active = true;
            Target target = await _context.Targets.FindAsync(mission.TargetId);
            target.Active = true;

            mission.Status = "assigned";
            await _context.SaveChangesAsync();

            var missions = await _context.Mission.
                Where(m => m.Status == "pussible" && (m.AgentId == mission.AgentId || m.TargetId == mission.TargetId)).
                ToListAsync();

            foreach (Mission mission1 in missions)
            {
                 _context.Mission.Remove(mission1);    
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

       

    }

    
}
