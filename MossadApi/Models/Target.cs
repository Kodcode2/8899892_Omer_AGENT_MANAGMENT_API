using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace MossadApi.Models
{
    public class Target
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string PhotoUrl { get; set; }
        [Range(0, 1000)]
        public int X_axis {  get; set; }

        [Range(0, 1000)]
        public int Y_axis { get; set; }

        public bool Eliminated { get; set; } =  false;

        public bool Active { get; set; } = false;
    }
}







//public class Target
//{
//    public int Id { get; set; }

//    [Column(TypeName = "json")]
//    public string LocationJson { get; set; }

//    [NotMapped]
//    public Dictionary<string, int> Location
//    {
//        get => string.IsNullOrEmpty(LocationJson) ? null : JsonSerializer.Deserialize<Dictionary<string, int>>(LocationJson);
//        set => LocationJson = JsonSerializer.Serialize(value);
//    }