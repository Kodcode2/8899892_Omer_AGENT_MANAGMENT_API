﻿using MossadApi.Models;
using System.Linq.Expressions;

namespace MossadApi
{
    public  class calculation : Icalculatlocation
    {
        public async Task<Agents> AgentLocation(Agents agents, Dictionary<string,string> direction) 
        {
            int x = agents.X_axis;
            int y = agents.Y_axis;
            List<int> list = await caculat(x, y, direction);
            agents.X_axis = list[0];
            agents.Y_axis = list[1];
            return agents;
        }

        public async Task<Target> TargetLocation(Target target, Dictionary<string, string> direction)
        {
            int x = target.X_axis;
            int y = target.Y_axis;
            List<int> list = await caculat(x, y, direction);
            target.X_axis = list[0];
            target.Y_axis = list[1];    
            return target;
        }

        private async Task <List<int>> caculat(int x, int y, Dictionary<string, string> direction )
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

        public async Task<double> timetotarget(Target target, Agents agent)
        {
            int x = target.X_axis; 
            int y = target.Y_axis;
            double distans =  Math.Sqrt(Math.Pow(target.X_axis - agent.X_axis, 2) + Math.Pow(target.Y_axis - agent.Y_axis, 2));
            double timelaft = distans / 5;
            return timelaft;
        }


        //אתגר, מה להחזיר?
        public async Task<Dictionary<string, string>> directioncalculation(Target target, Agents agent)
        {
            int x =  agent.X_axis - target.X_axis;
            int y = agent.Y_axis - target.Y_axis;
            Dictionary<string, string> direction = new Dictionary<string, string>();
            string str = "unknown";

            if (x < 0 && y < 0)
            {
                str = "sw"; 
            }
            else if (x < 0 && y == 0)
            {
                str = "w"; 
            }
            else if (x < 0 && y < 0)
            {
                str = "nw"; 
            }
            else if (x == 0 && y > 0)
            {
                str = "s"; 
            }
            else if (x == 0 && y == 0)
            {
                str = "touchdown"; 
            }
            else if (x == 0 && y > 0)
            {
                str = "n"; 
            }
            else if (x > 0 && y < 0)
            {
                str = "se"; 
            }
            else if (x > 0 && y == 0)
            {
                str = "e";
            }
            else if (x > 0 && y > 0)
            {
                str = "ne"; 
            }

            direction["direction"] = str;
            return direction;
            
        }


        public async Task<Agents> movment(Target target, Agents agent)
        {

            if (agent.X_axis > target.X_axis)
            {
                agent.X_axis -= 1;
            }
            else if (agent.X_axis < target.X_axis)
            {
                agent.X_axis += 1;
            }
            if (agent.Y_axis > target.Y_axis)
            {
                agent.Y_axis -= 1;
            }
            else if (agent.Y_axis < target.Y_axis)
            {
                agent.Y_axis += 1;
            }
            return agent;
        }
    }
}
