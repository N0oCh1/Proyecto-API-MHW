using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyecto_API_MHW.Models;

namespace Proyecto_API_MHW.Contexts;

public partial class MhwApiContext : DbContext
{
    public MhwApiContext()
    {
    }

    public MhwApiContext(DbContextOptions<MhwApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bioma> Biomas { get; set; }

    public virtual DbSet<CategoriaMonstro> CategoriaMonstros { get; set; }

    public virtual DbSet<Debilidade> Debilidades { get; set; }

    public virtual DbSet<Elemento> Elementos { get; set; }

    public virtual DbSet<ImagenMonstro> ImagenMonstros { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<MgDebilidade> MgDebilidades { get; set; }

    public virtual DbSet<MgElemento> MgElementos { get; set; }

    public virtual DbSet<MonstroGrande> MonstroGrandes { get; set; }

    public virtual DbSet<Rango> Rangos { get; set; }

    public virtual DbSet<VMonstroGrande> VMonstroGrandes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bioma>(entity =>
        {
            entity.HasKey(e => e.IdBioma).HasName("biomas_pkey");

            entity.ToTable("biomas");

            entity.Property(e => e.IdBioma).HasColumnName("id_bioma");
            entity.Property(e => e.NombreBioma)
                .HasMaxLength(50)
                .HasColumnName("nombre_bioma");

            entity.HasMany(d => d.IdMonstros).WithMany(p => p.IdBiomas)
                .UsingEntity<Dictionary<string, object>>(
                    "MgBioma",
                    r => r.HasOne<MonstroGrande>().WithMany()
                        .HasForeignKey("IdMonstro")
                        .HasConstraintName("fk_idmonstro"),
                    l => l.HasOne<Bioma>().WithMany()
                        .HasForeignKey("IdBioma")
                        .HasConstraintName("fk_idbioma"),
                    j =>
                    {
                        j.HasKey("IdBioma", "IdMonstro").HasName("mg_bioma_pkey");
                        j.ToTable("mg_bioma");
                        j.IndexerProperty<int>("IdBioma").HasColumnName("id_bioma");
                        j.IndexerProperty<int>("IdMonstro").HasColumnName("id_monstro");
                    });
        });

        modelBuilder.Entity<CategoriaMonstro>(entity =>
        {
            entity.HasKey(e => e.IdTipoMonstro).HasName("categoria_monstro_pkey");

            entity.ToTable("categoria_monstro");

            entity.Property(e => e.IdTipoMonstro).HasColumnName("id_tipo_monstro");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Debilidade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("debilidades_pkey");

            entity.ToTable("debilidades");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Eficacia).HasColumnName("eficacia");
            entity.Property(e => e.IdElemento).HasColumnName("id_elemento");
            entity.Property(e => e.IdMonstro).HasColumnName("id_monstro");
        });

        modelBuilder.Entity<Elemento>(entity =>
        {
            entity.HasKey(e => e.IdElemento).HasName("elementos_pkey");

            entity.ToTable("elementos");

            entity.Property(e => e.IdElemento).HasColumnName("id_elemento");
            entity.Property(e => e.Elemento1)
                .HasMaxLength(20)
                .HasColumnName("elemento");

            entity.HasMany(d => d.IdMonstros).WithMany(p => p.IdElementos)
                .UsingEntity<Dictionary<string, object>>(
                    "ElementoMonstro",
                    r => r.HasOne<MonstroGrande>().WithMany()
                        .HasForeignKey("IdMonstro")
                        .HasConstraintName("fk_idmonstro"),
                    l => l.HasOne<Elemento>().WithMany()
                        .HasForeignKey("IdElemento")
                        .HasConstraintName("fk_idelemento"),
                    j =>
                    {
                        j.HasKey("IdElemento", "IdMonstro").HasName("elemento_monstro_pkey");
                        j.ToTable("elemento_monstro");
                        j.IndexerProperty<int>("IdElemento").HasColumnName("id_elemento");
                        j.IndexerProperty<int>("IdMonstro").HasColumnName("id_monstro");
                    });
        });

        modelBuilder.Entity<ImagenMonstro>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("imagen_monstro_pkey");

            entity.ToTable("imagen_monstro");

            entity.Property(e => e.IdImagen).HasColumnName("id_imagen");
            entity.Property(e => e.IconUrl)
                .HasMaxLength(100)
                .HasColumnName("icon_url");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(100)
                .HasColumnName("image_url");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.IdItem).HasName("items_pkey");

            entity.ToTable("items");

            entity.Property(e => e.IdItem).HasColumnName("id_item");
            entity.Property(e => e.DescripcionItem)
                .HasMaxLength(255)
                .HasColumnName("descripcion_item");
            entity.Property(e => e.IdMonstro).HasColumnName("id_monstro");
            entity.Property(e => e.NombreItem)
                .HasMaxLength(50)
                .HasColumnName("nombre_item");

            entity.HasOne(d => d.IdMonstroNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.IdMonstro)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_items");
        });

        modelBuilder.Entity<MgDebilidade>(entity =>
        {
            entity.HasKey(e => new { e.IdElemento, e.IdMonstro }).HasName("mg_debilidades_pkey");

            entity.ToTable("mg_debilidades");

            entity.Property(e => e.IdElemento).HasColumnName("id_elemento");
            entity.Property(e => e.IdMonstro).HasColumnName("id_monstro");
            entity.Property(e => e.Eficacia).HasColumnName("eficacia");

            entity.HasOne(d => d.IdElementoNavigation).WithMany(p => p.MgDebilidades)
                .HasForeignKey(d => d.IdElemento)
                .HasConstraintName("fk_idmelemento");

            entity.HasOne(d => d.IdMonstroNavigation).WithMany(p => p.MgDebilidades)
                .HasForeignKey(d => d.IdMonstro)
                .HasConstraintName("fk_idmonstro");
        });

        modelBuilder.Entity<MgElemento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mg_elemento_pkey");

            entity.ToTable("mg_elemento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdElemento).HasColumnName("id_elemento");
            entity.Property(e => e.IdMonstro).HasColumnName("id_monstro");
        });

        modelBuilder.Entity<MonstroGrande>(entity =>
        {
            entity.HasKey(e => e.IdMonstrog).HasName("monstro_grande_pkey");

            entity.ToTable("monstro_grande");

            entity.Property(e => e.IdMonstrog).HasColumnName("id_monstrog");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdImagen).HasColumnName("id_imagen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");
            entity.Property(e => e.Vida).HasColumnName("vida");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.MonstroGrandes)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_categoria");

            entity.HasOne(d => d.IdImagenNavigation).WithMany(p => p.MonstroGrandes)
                .HasForeignKey(d => d.IdImagen)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_imagen");
        });

        modelBuilder.Entity<Rango>(entity =>
        {
            entity.HasKey(e => e.IdRango).HasName("rangos_pkey");

            entity.ToTable("rangos");

            entity.Property(e => e.IdRango).HasColumnName("id_rango");
            entity.Property(e => e.Rango1)
                .HasMaxLength(20)
                .HasColumnName("rango");

            entity.HasMany(d => d.IdMonstros).WithMany(p => p.IdRangos)
                .UsingEntity<Dictionary<string, object>>(
                    "MgRango",
                    r => r.HasOne<MonstroGrande>().WithMany()
                        .HasForeignKey("IdMonstro")
                        .HasConstraintName("fk_idmonstro"),
                    l => l.HasOne<Rango>().WithMany()
                        .HasForeignKey("IdRango")
                        .HasConstraintName("fk_idrango"),
                    j =>
                    {
                        j.HasKey("IdRango", "IdMonstro").HasName("mg_rango_pkey");
                        j.ToTable("mg_rango");
                        j.IndexerProperty<int>("IdRango").HasColumnName("id_rango");
                        j.IndexerProperty<int>("IdMonstro").HasColumnName("id_monstro");
                    });
        });

        modelBuilder.Entity<VMonstroGrande>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_monstro_grande");

            entity.Property(e => e.IdMonstrog).HasColumnName("id_monstrog");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .HasColumnName("tipo");
            entity.Property(e => e.Vida).HasColumnName("vida");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
