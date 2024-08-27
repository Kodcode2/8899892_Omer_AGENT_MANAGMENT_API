using MossadApi.Models;

namespace MossadApi.@interface
{
    public interface ISetmission
    {
        Task Set();
        Task chektarget(Target target);
        Task chekagent(Agents agent);
    }
}
