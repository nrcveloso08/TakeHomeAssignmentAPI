using System.ComponentModel.DataAnnotations;

namespace TakeHomeAssignmentAPI.Models
{
    public class Packaging
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Material { get; set; }
        public string Dimensions { get; set; }
        [Required]
        public string Type { get; set; }
        public double Weight { get; set; }
    }
}
