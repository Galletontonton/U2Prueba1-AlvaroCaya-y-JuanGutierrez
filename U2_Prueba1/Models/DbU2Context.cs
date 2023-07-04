using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace U2_Prueba1.Models;

public partial class DbU2Context : DbContext
{
    public DbU2Context()
    {
    }

    public DbU2Context(DbContextOptions<DbU2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<AsignaturasEstudiante> AsignaturasEstudiantes { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Nota> Notas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignaturas");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Codigo).HasMaxLength(45);
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.FechaActualizacion).HasColumnName("Fecha_Actualizacion");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<AsignaturasEstudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignaturas_estudiantes");

            entity.HasIndex(e => e.AsignaturasId, "fk_estudiantes_has_asignaturas_asignaturas1_idx");

            entity.HasIndex(e => e.EstudiantesId, "fk_estudiantes_has_asignaturas_estudiantes_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.AsignaturasId)
                .HasColumnType("int(11)")
                .HasColumnName("asignaturas_Id");
            entity.Property(e => e.EstudiantesId)
                .HasColumnType("int(11)")
                .HasColumnName("estudiantes_Id");
            entity.Property(e => e.FechaRegistro).HasColumnName("Fecha_Registro");

            entity.HasOne(d => d.Asignaturas).WithMany(p => p.AsignaturasEstudiantes)
                .HasForeignKey(d => d.AsignaturasId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_estudiantes_has_asignaturas_asignaturas1");

            entity.HasOne(d => d.Estudiantes).WithMany(p => p.AsignaturasEstudiantes)
                .HasForeignKey(d => d.EstudiantesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_estudiantes_has_asignaturas_estudiantes");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estudiantes");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Edad).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaNacimiento).HasColumnName("Fecha_Nacimiento");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(45);
            entity.Property(e => e.Rut).HasMaxLength(45);
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notas");

            entity.HasIndex(e => e.AsignaturasId, "fk_notas_asignaturas1_idx");

            entity.HasIndex(e => e.EstudiantesId, "fk_notas_estudiantes1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.AsignaturasId)
                .HasColumnType("int(11)")
                .HasColumnName("asignaturas_Id");
            entity.Property(e => e.EstudiantesId)
                .HasColumnType("int(11)")
                .HasColumnName("estudiantes_Id");
            entity.Property(e => e.Nota1).HasColumnName("Nota");

            entity.HasOne(d => d.Asignaturas).WithMany(p => p.Nota)
                .HasForeignKey(d => d.AsignaturasId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_notas_asignaturas1");

            entity.HasOne(d => d.Estudiantes).WithMany(p => p.Nota)
                .HasForeignKey(d => d.EstudiantesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_notas_estudiantes1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
