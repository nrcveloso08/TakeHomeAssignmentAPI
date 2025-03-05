﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakeHomeAssignmentAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Foreign key reference to Packaging
        public int PackagingId { get; set; }

        // Navigation property
        public Packaging Packaging { get; set; }
    }
}
