using MossadApi.Models;
using MossadApi.DAL;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MossadApi.@interface;

namespace MossadApi.@interface
{
    public class SetMission : ISetmission
    {
        private readonly DBContext _context;


        public SetMission(DBContext context)
        {
            _context = context;
        }




        //מציע המשימות
        public async Task Set()
        {
            var targets = _context.Targets.
                Where(t => t.Active == false).
                ToList();
            var agents = _context.Agents.
                Where(a => a.Active == false).
                ToList();

            foreach (var target in targets)
            {
                foreach (var agent in agents)
                {

                    double distanse = Math.Sqrt(Math.Pow(target.X_axis - agent.X_axis, 2) + Math.Pow(target.Y_axis - agent.Y_axis, 2));
                    if (distanse < 200 && distanse > 0)
                    {
                        var missionchek = await _context.Mission
                       .FirstOrDefaultAsync(m => m.AgentId == agent.Id && m.TargetId == target.Id);

                        if (missionchek == null)
                        {
                            Mission mission = new Mission();
                            mission.AgentId = agent.Id;
                            mission.TargetId = target.Id;
                            mission.TotalTime = distanse / 5;
                            _context.Mission.Add(mission);
                            agent.assigned = true;
                        }
                    }
                }
            }
            _context.SaveChanges();
        }

        public async Task chektarget(Target target)
        {
            List<Mission> relatedMissions = await _context.Mission
            .Where(m => m.TargetId == target.Id)
            .ToListAsync();

            foreach (Mission mission in relatedMissions)
            {
                var agent = await _context.Agents.FindAsync(mission.AgentId);

                double distance = await Distance(target, agent);
                if (distance > 200 || distance <= 0)
                {
                    _context.Mission.Remove(mission);
                    agent.assigned = false;
                }

            }
            await _context.SaveChangesAsync();
        }

        public async Task chekagent(Agents agent)
        {
            List<Mission> relatedMissions = await _context.Mission
            .Where(m => m.AgentId == agent.Id)
            .ToListAsync();

            foreach (Mission mission in relatedMissions)
            {
                Target target = await _context.Targets.FindAsync(mission.TargetId);

                double distance = await Distance(target, agent);
                if (distance > 200 || distance <= 0)
                {
                    _context.Mission.Remove(mission);
                    agent.assigned = false;
                }

            }
            await _context.SaveChangesAsync();
        }


        private async Task<double> Distance(Target? target, Agents agent)
        {
            return Math.Sqrt(Math.Pow(target.X_axis - agent.X_axis, 2) + Math.Pow(target.Y_axis - agent.Y_axis, 2));
        }
    }
}
