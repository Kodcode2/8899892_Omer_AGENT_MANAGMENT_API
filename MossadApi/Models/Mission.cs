using System.ComponentModel.DataAnnotations;

namespace MossadApi.Models
{
    public class Mission
    {
        [Key]
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int TargetId { get; set; }
        public float Timelaft { get; set; }
        public float TotalTime { get; set; }
    }
}
