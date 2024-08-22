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
            var targets = _context.Targets.ToList();
            var agents = _context.Agents.ToList();

            foreach (var target in targets)
            {
                foreach (var agent in agents)
                {
                    if (Math.Sqrt(Math.Pow(target.X_axis - agent.X_axis, 2) + Math.Pow(target.Y_axis - agent.Y_axis, 2)) < 200)
                    {
                        Mission mission = new Mission();
                        mission.Status = "possible";
                        mission.AgentId = agent.Id;
                        mission.TargetId = target.Id;
                        Console.WriteLine("abnb['samoi[eabmnoiabtno[abntrioi[tra  ", mission);
                        _context.Mission.Add(mission);
                    }
                    _context.SaveChanges();
                }
            }
        }
    }
}
