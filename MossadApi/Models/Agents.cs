using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MossadApi.Models
{
    public class Agents
    {
        [Key]
        public int Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public int X_axis { get; set; }

        public int Y_axis { get; set; }

        public bool Active { get; set; } = false;
    }
}
