using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public partial class ProyectoDAWAContext : DbContext
{
    public ProyectoDAWAContext()
    {
    }

    public ProyectoDAWAContext(DbContextOptions<ProyectoDAWAContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comisione> Comisiones { get; set; }

    public virtual DbSet<EstudiantesPropuesta> EstudiantesPropuestas { get; set; }

    public virtual DbSet<HistorialPropuesta> HistorialPropuestas { get; set; }

    public virtual DbSet<MiembrosComision> MiembrosComisions { get; set; }

    public virtual DbSet<Periodo> Periodos { get; set; }

    public virtual DbSet<Propuesta> Propuestas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=ProyectoDAWA;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comisione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comision__3214EC07B415C4AB");

            entity.ToTable(tb => tb.HasTrigger("VerificarGestorId"));

            entity.HasIndex(e => e.Nombre, "UQ__Comision__75E3EFCFAD662F98").IsUnique();

            entity.Property(e => e.FechaCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Proposito).HasColumnType("text");

            entity.HasOne(d => d.Gestor).WithMany(p => p.Comisiones)
                .HasForeignKey(d => d.GestorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comisione__Gesto__5535A963");
        });

        modelBuilder.Entity<EstudiantesPropuesta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estudian__3214EC0786BC2799");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("VerificarCantidadEstudiantes");
                    tb.HasTrigger("VerificarRolEstudiante");
                });

            entity.HasIndex(e => e.EstudianteId, "UQ__Estudian__6F7682D946A751BE").IsUnique();

            entity.HasIndex(e => new { e.PropuestaId, e.EstudianteId }, "UQ__Estudian__DDF1104EB91060DB").IsUnique();

            entity.HasOne(d => d.Estudiante).WithOne(p => p.EstudiantesPropuesta)
                .HasForeignKey<EstudiantesPropuesta>(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estudiant__Estud__48CFD27E");

            entity.HasOne(d => d.Propuesta).WithMany(p => p.EstudiantesPropuesta)
                .HasForeignKey(d => d.PropuestaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estudiant__Propu__49C3F6B7");
        });

        modelBuilder.Entity<HistorialPropuesta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC0793F7C185");

            entity.Property(e => e.ComentarioEstudiante).HasColumnType("text");
            entity.Property(e => e.DireccionArchivo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EstadoAprobacion).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.FechaEnvio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ObservacionRevisor).HasColumnType("text");

            entity.HasOne(d => d.Propuesta).WithMany(p => p.HistorialPropuesta)
                .HasForeignKey(d => d.PropuestaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Propu__5070F446");
        });

        modelBuilder.Entity<MiembrosComision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Miembros__3214EC072978BA9F");

            entity.ToTable("MiembrosComision", tb => tb.HasTrigger("VerificarCoordinadorComisionId"));

            entity.HasOne(d => d.CoordinadorComision).WithMany(p => p.InverseCoordinadorComision)
                .HasForeignKey(d => d.CoordinadorComisionId)
                .HasConstraintName("FK__MiembrosC__Coord__59063A47");

            entity.HasOne(d => d.MiembrosComisionNavigation).WithMany(p => p.MiembrosComisions)
                .HasForeignKey(d => d.MiembrosComisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MiembrosC__Miemb__5812160E");
        });

        modelBuilder.Entity<Periodo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Periodos__3214EC076DDCFF10");

            entity.HasIndex(e => e.FechaFin, "UQ__Periodos__05C062C614E4DB2B").IsUnique();

            entity.HasIndex(e => e.CicloActual, "UQ__Periodos__9868096BEE8DAEB6").IsUnique();

            entity.HasIndex(e => e.FechaInicio, "UQ__Periodos__C99131D1DA2AE30E").IsUnique();

            entity.Property(e => e.CicloActual)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValueSql("(NULL)");
        });

        modelBuilder.Entity<Propuesta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Propuest__3214EC0747377F1E");

            entity.ToTable(tb => tb.HasTrigger("VerificarRevisorId"));

            entity.Property(e => e.Calificacion).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.FechaCreación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Revisor).WithMany(p => p.Propuesta)
                .HasForeignKey(d => d.RevisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Propuesta__Revis__440B1D61");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC0732911A5D");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A1939A57205").IsUnique();

            entity.HasIndex(e => e.Nombre, "UQ__Usuarios__75E3EFCF1416C4D7").IsUnique();

            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
