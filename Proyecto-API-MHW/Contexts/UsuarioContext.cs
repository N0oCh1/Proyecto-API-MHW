using Microsoft.EntityFrameworkCore;
using Proyecto_API_MHW.Models;

namespace Proyecto_API_MHW.Contexts;

    public partial class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options)
        : base(options)
        {
        }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario).HasName("usuario_pkey");

                entity.ToTable("usuario");

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");
                entity.Property(e => e.Nombreusuario)
                    .HasMaxLength(50)
                    .HasColumnName("nombreusuario");
                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");
            });
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

