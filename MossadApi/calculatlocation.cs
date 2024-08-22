using MossadApi.Models;

namespace MossadApi
{
    public interface Icalculatlocation
    {
        Agents AgentLocation(Agents agents, Dictionary<string, string> dict);
        Target TargetLocation(Target target, Dictionary<string, string> dict);

    }
}
