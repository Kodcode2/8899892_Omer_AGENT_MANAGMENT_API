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
                    if (agents.X_axis == target.X_axis && agents.Y_axis == target.Y_axis)
                    {
                        mission.Status = "finish";
                        mission.Timelaft = 0;
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
            Target target = await _context.Targets.FindAsync(mission.TargetId);

            //ווידוא סופי שהמרחק מתאים לפני ההפעלה
            double distence = await _icalculatlocation.timetotarget(target, agents);
            if (distence * 5 > 200)
            {
                return BadRequest();
               
            }

            mission.Status = "assigned";
            target.Active = true;
            agents.Active = true;
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

        [HttpGet("meneger")]
        public async Task<IActionResult> details()
        {
            Details details = new Details();
            details.Total_Missions = await _context.Mission.CountAsync();
            details.Active_Missions = await _context.Mission.Where(m => m.Status == "assigned").CountAsync();
            details.Active_Agents = await _context.Agents.Where(a => a.Active == true).CountAsync();
            details.Total_Agents = await _context.Agents.CountAsync();
            details.Total_Targets = await _context.Targets.CountAsync();
            //details.Dead_Targets = await _context.Targets.Where(t => t.Alive == false).CountAsync();
            details.Ratio_Agents_To_Taegets = 1; // details.Total_Agents / details.Total_Targets;
            details.Possibal_Agents_To_Targets = await _context.Agents.Where(a => a.assigned == true).CountAsync();
            return StatusCode(200, details);
        }
    }


}
