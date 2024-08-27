using MossadApi.Models;

namespace MossadApi.@interface
{
    public interface Icalculatlocation
    {
        Task<Agents> AgentLocation(Agents agents, Dictionary<string, string> dict);
        Task<Target> TargetLocation(Target target, Dictionary<string, string> dict);
        Task<double> timetotarget(Target target, Agents agent);
        Task<Agents> movment(Target target, Agents agent);
    }
}
