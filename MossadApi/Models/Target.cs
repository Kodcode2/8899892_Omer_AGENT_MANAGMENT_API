using System.ComponentModel.DataAnnotations;

namespace MossadApi.Models
{
    public class Target
    {
        [Key]
        public int Id;

        public string Name {  get; set; }

        public string Description { get; set; }

        public Dictionary<string, int> Location { get; set; }
       
        public bool Alive = true;
    }
}
