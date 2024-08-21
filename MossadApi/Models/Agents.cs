using System.ComponentModel.DataAnnotations;

namespace MossadApi.Models
{
    public class Agents
    {

        [Key]
        public int Id { get; set; }

        //add image

        [Required]
        public string Name { get; set; }
        [Required]
        public Dictionary<string, int> Location { get; set; }
        [Required]
        public bool Active = false;
    }
}
