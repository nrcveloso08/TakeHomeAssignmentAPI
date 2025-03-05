using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TakeHomeAssignmentAPI.Models;

namespace TakeHomeAssignmentAPI.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Seed Packaging Data
            var packagingList = new List<Packaging>();
            for (int i = 1; i <= 100; i++)
            {
                packagingList.Add(new Packaging
                {
                    Id = i,
                    Material = $"Material {i}",
                    Dimensions = $"{i}x{i}x{i} cm",
                    Type = i % 2 == 0 ? "Box" : "Bag",
                    Weight = i * 0.5
                });
            }
            modelBuilder.Entity<Packaging>().HasData(packagingList);

            // Seed Product Data
            var productList = new List<Product>();
            for (int i = 1; i <= 100; i++)
            {
                productList.Add(new Product
                {
                    Id = i,
                    Name = $"Product {i}",
                    Price = i * 10.99m,
                    PackagingId = (i % 100) + 1 // Assigns a valid PackagingId
                });
            }
            modelBuilder.Entity<Product>().HasData(productList);

            // Seed Packaging Hierarchy Data (10 Relationships)
            var packagingHierarchyList = new List<Packaging_Hierarchy>
            {
                new Packaging_Hierarchy { ParentPackagingId = 1, ChildPackagingId = 2 },
                new Packaging_Hierarchy { ParentPackagingId = 1, ChildPackagingId = 3 },
                new Packaging_Hierarchy { ParentPackagingId = 2, ChildPackagingId = 4 },
                new Packaging_Hierarchy { ParentPackagingId = 2, ChildPackagingId = 5 },
                new Packaging_Hierarchy { ParentPackagingId = 3, ChildPackagingId = 6 },
                new Packaging_Hierarchy { ParentPackagingId = 3, ChildPackagingId = 7 },
                new Packaging_Hierarchy { ParentPackagingId = 4, ChildPackagingId = 8 },
                new Packaging_Hierarchy { ParentPackagingId = 5, ChildPackagingId = 9 },
                new Packaging_Hierarchy { ParentPackagingId = 6, ChildPackagingId = 10 },
                new Packaging_Hierarchy { ParentPackagingId = 7, ChildPackagingId = 11 }
            };

            modelBuilder.Entity<Packaging_Hierarchy>().HasData(packagingHierarchyList);
        }
    }
}
