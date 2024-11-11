using Microsoft.EntityFrameworkCore;
using Proyecto_API_MHW.Models;

namespace Proyecto_API_MHW.Contexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<MonstroGrandes> MonstroGrandes { get; set; }
        public DbSet<vMonstroGrandes> vMonstroGrandes {get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        //asc

    }
}
