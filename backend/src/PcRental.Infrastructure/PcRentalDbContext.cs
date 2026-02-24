using Microsoft.EntityFrameworkCore;
using PcRental.Domain.Entities;

namespace PcRental.Infrastructure
{
    public class PcRentalDbContext : DbContext
    {
        public PcRentalDbContext(DbContextOptions<PcRentalDbContext> options)
            : base(options) {}
        
        public DbSet<Computer> Computers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Computer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.IsAvailable).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}