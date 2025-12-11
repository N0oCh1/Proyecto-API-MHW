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

                // Unique indexes (nombreusuario y correo)
                entity.HasIndex(e => e.Nombreusuario).IsUnique().HasDatabaseName("ux_usuario_nombreusuario");
                entity.HasIndex(e => e.Correo).IsUnique().HasDatabaseName("ux_usuario_correo");

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");
                entity.Property(e => e.Nombreusuario)
                    .HasMaxLength(50)
                    .IsRequired()
                    .HasColumnName("nombreusuario");
                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsRequired()
                    .HasColumnName("password");

                // Nuevas columnas
                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsRequired()
                    .HasColumnName("correo");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsRequired()
                    .HasColumnName("nombre");
                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsRequired()
                    .HasColumnName("apellido");

                // Check constraints similares a la definición SQL original
                entity.HasCheckConstraint("check_password_length", "length(password) >= 8");
                entity.HasCheckConstraint("check_email_format", "correo ~* '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$'");
            });
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

