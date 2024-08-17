using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Migrations
{
    public class XDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<X> X { get; set; }

        public XDbContext(DbContextOptions<XDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
                entity.Property(e => e.Username).IsRequired();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.City).IsRequired();
                entity.Property(e => e.State).IsRequired();
            });

            modelBuilder.Entity<X>(entity =>
            {
                entity.HasKey(e => e.XId);
                entity.Property(e => e.XId).ValueGeneratedOnAdd();
                entity.Property(e => e.Text).IsRequired();
                entity.Property(e => e.Username).IsRequired();
            });
        }
    }
}
