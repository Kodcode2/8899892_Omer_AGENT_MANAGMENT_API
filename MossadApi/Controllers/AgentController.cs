using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MossadApi.DAL;

using Microsoft.EntityFrameworkCore;
using MossadApi.Models;
using MossadApi.@interface;

namespace MossadApi.Controllers
{
    [Route("/[controller]s")]
    [ApiController]
    public class agentController : ControllerBase
    {
        private readonly Icalculatlocation _icalculatlocation;
        private readonly DBContext _context;
        private readonly ILogger<agentController> _logger;
        private readonly ISetmission _setmission;

        public agentController(ILogger<agentController> logger, DBContext context, Icalculatlocation icalculatlocation, ISetmission setmission)
        {

            this._context = context;
            this._logger = logger;
            this._icalculatlocation = icalculatlocation;
            _setmission = setmission;
        }

        //שרת סימולציה בלבד
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAgent([FromBody] Agents agent)
        {
            this._context.Agents.Add(agent);
            await _setmission.Set();
            await this._context.SaveChangesAsync();
           
            return StatusCode
                (StatusCodes.Status201Created, agent);
        }



        //שרת סימולציה ומנהל בקרה
        //רשימת מטרות
        [HttpGet]
        public async Task<IActionResult> getagents()
        {
            var agents = await this._context.Agents.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, agents);
        }
        


        //שרת סימולציה בלבד
        //קביעת מיקום התחלתי
        //יצירת משימה 
        [HttpPut("{id}/pin")]
        public async Task<IActionResult> putlocation(int id, Dictionary<string,int> location)
        {
            Agents agent = await _context.Agents.FindAsync(id);
            if (agent == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            
            agent.X_axis = location["x"];
            agent.Y_axis = location["y"];
           
            await this._context.SaveChangesAsync();
            return StatusCode(200);
        }



        //שרת סימולציה בלבד
        //תזוזה
        [HttpPut("{id}/move")]
        public async Task<IActionResult> updatlocation(int id, [FromBody] Dictionary<string, string> move)
        {
            Agents agent = await _context.Agents.FindAsync(id);
            if (agent == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            //הודעת שגיאה במקרה שהסוכן מצוות
            if (agent.Active == true) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { messege = "the agent already in a mission" });
            }          
            //ביצוע הזזה במטריצה 
            agent =  await _icalculatlocation.AgentLocation(agent, move);

            return StatusCode(200, new {agent = agent});

        }


    }

    


}
