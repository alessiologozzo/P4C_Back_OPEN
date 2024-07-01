using Microsoft.EntityFrameworkCore;
using P4C_Back.Models;

namespace P4C_Back.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Criterio> Criteri { get; set; }
        public DbSet<Kpi> Kpis { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Piattaforma> Piattaforme { get; set; }
        public DbSet<Canale> Canali { get; set; }
        public DbSet<Abilitazione> Abilitazioni { get; set; }
        public DbSet<ValoreEnum> ValoreEnum { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kpi>()
            .HasMany(e => e.Criteri)
            .WithMany(e => e.Kpis)
            .UsingEntity<Dictionary<string, object>>(
                "tbl_kpicriteri",
                e => e.HasOne<Criterio>().WithMany().HasForeignKey("IdCriterio"),
                e => e.HasOne<Kpi>().WithMany().HasForeignKey("IdKpi"));

            modelBuilder.Entity<Report>()
                .HasMany(e => e.Kpis)
                .WithMany(e => e.Reports)
                .UsingEntity<Dictionary<string, object>>(
                "tbl_reportkpi",
                e => e.HasOne<Kpi>().WithMany().HasForeignKey("IdKpi"),
                e => e.HasOne<Report>().WithMany().HasForeignKey("IdReport"));

            modelBuilder.Entity<Report>()
                .HasMany(e => e.Canali)
                .WithMany(e => e.Reports)
                .UsingEntity<Dictionary<string, object>>(
                "tbl_reportcanale",
                e => e.HasOne<Canale>().WithMany().HasForeignKey("IdCanale"),
                e => e.HasOne<Report>().WithMany().HasForeignKey("IdReport"));

            modelBuilder.Entity<Canale>()
                .HasOne(c => c.Piattaforma)
                .WithMany(p => p.Canali)
                .HasForeignKey(c => c.FkPiattaforma);

            modelBuilder.Entity<ValoreEnum>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
