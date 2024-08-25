using MossadApi.Models;

namespace MossadApi
{
    public interface Icalculatlocation
    {
        Task <Agents> AgentLocation(Agents agents, Dictionary<string, string> dict);
        Task <Target> TargetLocation(Target target, Dictionary<string, string> dict);
        //Task<Dictionary<string, string>> directioncalculation(Target target, Agents agent);
        Task<double> timetotarget(Target target, Agents agent);
        Task<Agents> movment(Target target, Agents agent);
    }
}
