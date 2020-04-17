using Microsoft.EntityFrameworkCore;

namespace IronHasura.Data
{
    public class IronHasuraDbContext : DbContext
    {
        public IronHasuraDbContext()
        {

        }

        public IronHasuraDbContext(DbContextOptions<IronHasuraDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Analytic> Analytic { get; set; }
        public virtual DbSet<RemoteConfig> RemoteConfig { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Analytic>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Dimension)
                    .IsRequired()
                    .HasColumnType("jsonb");
            });

            modelBuilder.Entity<RemoteConfig>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("jsonb");
            });
        }
    }
}