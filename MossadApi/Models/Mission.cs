using System.ComponentModel.DataAnnotations;

namespace MossadApi.Models
{
    public class Mission
    {
        [Key]
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int TargetId { get; set; }
        public double Timelaft { get; set; }
        public double TotalTime { get; set; }

        [AllowedValues("assigned", "finish", "pussible")]
        public string Status { get; set; } = "pussible";
    }
}
