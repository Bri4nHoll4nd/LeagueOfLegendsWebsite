using Microsoft.EntityFrameworkCore;

namespace HIOF.Net.Gruppe9.LeaugeVersionDB.Data
{
    public class VersionDBContext : DbContext
    {
        public DbSet<Version> Versions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=VersionDB;Integrated Security=True;TrustServerCertificate=True");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Version>(mb =>
            {
                mb.Property(version => version.Id);
                mb.Property(version => version.Name);

                mb.HasKey(version => version.Id);
            });
        }
    }
}
