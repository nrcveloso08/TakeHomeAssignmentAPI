using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakeHomeAssignmentAPI.Models
{
    public class Packaging_Hierarchy
    {
        [ForeignKey("ParentPackaging")]
        public int ParentPackagingId { get; set; }

        [ForeignKey("ChildPackaging")]
        public int ChildPackagingId { get; set; }

        // Navigation properties
        public Packaging ParentPackaging { get; set; }
        public Packaging ChildPackaging { get; set; }
    }
}
