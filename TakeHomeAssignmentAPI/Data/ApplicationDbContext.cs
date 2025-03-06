using Microsoft.EntityFrameworkCore;
using TakeHomeAssignmentAPI.Models;

namespace TakeHomeAssignmentAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Packaging> Packagings { get; set; }
        public DbSet<Packaging_Hierarchy> Packaging_Hierarchies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Packaging_Hierarchy>()
                .HasKey(ph => new { ph.ParentPackagingId, ph.ChildPackagingId });

           
            modelBuilder.Entity<Packaging_Hierarchy>()
                .HasOne(ph => ph.ParentPackaging)
                .WithMany()
                .HasForeignKey(ph => ph.ParentPackagingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Packaging_Hierarchy>()
                .HasOne(ph => ph.ChildPackaging)
                .WithMany()
                .HasForeignKey(ph => ph.ChildPackagingId)
                .OnDelete(DeleteBehavior.Restrict);

           
            SeedData.Seed(modelBuilder);
        }
    }
}