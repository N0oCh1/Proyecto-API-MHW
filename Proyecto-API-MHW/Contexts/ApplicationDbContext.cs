using Microsoft.EntityFrameworkCore;
using Proyecto_API_MHW.Models;

namespace Proyecto_API_MHW.Contexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<MonstroGrande> MonstroGrandes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Bioma> Biomas { get; set; }
        public DbSet<MgBioma> MgBiomas { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<Elemento> elementos { get; set; }
        public DbSet<MgElemento> mgElementos { get; set; }
        public DbSet<MgDebilidad> mgDebilidades { get; set; }
        public DbSet<Rango> rangos { get; set; }
        public DbSet<MgRango> mgRangos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MgBioma>().HasKey(e => new { e.id_bioma,e.id_monstro});
            modelBuilder.Entity<MgElemento>().HasKey(e => new { e.id_elemento, e.id_monstro });
            modelBuilder.Entity<MgDebilidad>().HasKey(e => new { e.id_elemento, e.id_monstro });
            modelBuilder.Entity<MgRango>().HasKey(e => new { e.id_rango, e.id_monstro });
        }
        
        //asc

    }
}
