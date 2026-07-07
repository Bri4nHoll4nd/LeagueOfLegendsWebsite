using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Net.Gruppe9.LeaugeChampionDB.Data
{
    public class ChampionDBContext : DbContext
    {
        public DbSet<Champion> Champions { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Stats> Stats { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(local); Initial Catalog=ChampionDB; Integrated Security=True; TrustServerCertificate=True");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Champion>(mb =>
            {
                mb.Property(champion => champion.Id);
                mb.Property(champion => champion.Version);
                mb.Property(champion => champion.RiotId);
                mb.Property(champion => champion.RiotKey);
                mb.Property(champion => champion.Name);
                mb.Property(champion => champion.Title);
                mb.Property(champion => champion.Blurb);

                mb.OwnsOne(champion => champion.Info);
                mb.OwnsOne(champion => champion.Image);
                mb.OwnsOne(champion => champion.Stats);

                mb.HasKey(champion =>champion.Id);
            });
        }
    }
}
