using MossadApi.Models;
using MossadApi.DAL;
using System.Reflection;

namespace MossadApi
{
    public  class SetMission: ISetmission
    {
        private readonly DBContext _context;


        public SetMission(DBContext context)
        {
            _context = context;
        }

        public void Set() 
        {
            var targets = _context.Targets.
                Where((t => t.Active == false)).
                ToList();
            var agents = _context.Agents.
                Where(a => a.Active == false).
                ToList();

            foreach (var target in targets)
            {
                foreach (var agent in agents)
                {
                    double distanse = Math.Sqrt(Math.Pow(target.X_axis - agent.X_axis, 2) + Math.Pow(target.Y_axis - agent.Y_axis, 2));
                    if (distanse < 200)
                    {
                        Mission mission = new Mission();
                        mission.AgentId = agent.Id;
                        mission.TargetId = target.Id;
                        mission.TotalTime = distanse / 5;
                        _context.Mission.Add(mission);                      
                    }
                   
                }
            }
            _context.SaveChanges();
        }
    }
}
