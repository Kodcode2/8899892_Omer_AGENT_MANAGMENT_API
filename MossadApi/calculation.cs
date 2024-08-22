using MossadApi.Models;
using System.Linq.Expressions;

namespace MossadApi
{
    public  class calculation : Icalculatlocation
    {
        public Agents AgentLocation(Agents agents, Dictionary<string,string> direction) 
        {
            int x = agents.X_axis;
            int y = agents.Y_axis;
            List<int> list = caculat(x, y,direction);
            agents.X_axis = list[0];
            agents.Y_axis = list[1];
            return agents;
        }

        public Target TargetLocation(Target target, Dictionary<string, string> direction)
        {
            int x = target.X_axis;
            int y = target.Y_axis;
            List<int> list = caculat(x, y, direction);
            target.X_axis = list[0];
            target.Y_axis = list[1];    
            return target;
        }

        private List<int> caculat(int x, int y, Dictionary<string, string> direction )
        {
            switch (direction["direction"])
            {
                case "nw":
                    y++;
                    x++;
                    break;
                case "n":
                    y++;
                    break;
                case "ne":
                    y++;
                    x--;
                    break;
                case "w":
                    x++;
                    break;
                case "e":
                    x--;
                    break;
                case "sw":
                    y--;
                    x++;
                    break;
                case "s":
                    y--;
                    break;
                case "se":
                    y--;
                    x--;
                    break;
                default:
                    break;
            }
            List<int> list = new List<int>()
                { x, y };
            return list;
        }

        private double timetotarget(Target target, Agents agent)
        {
            int x = target.X_axis; 
            int y = target.Y_axis;
            double distans =  Math.Sqrt(Math.Pow(target.X_axis - agent.X_axis, 2) + Math.Pow(target.Y_axis - agent.Y_axis, 2));
            double timelaft = distans / 5;
            return timelaft;
        }
        //אתגר, מה להחזיר?
        private double directioncalculation(Target target, Agents agent)
        {
            int xtarget = target.X_axis;
            int ytarget = target.Y_axis;
            int xagent = agent.X_axis;
            int yagent = agent.Y_axis;
            if (xtarget == xagent && ytarget == yagent)
            { 
                 
            }
        }
    }
}
