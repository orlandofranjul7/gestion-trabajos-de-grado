using System;
using System.Collections.Generic;
using GestionTrabajosDeGradoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTrabajosDeGradoAPI.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<asesor> asesors { get; set; }

    public DbSet<asesor_trabajo> asesor_trabajos { get; set; }

    public virtual DbSet<director> directors { get; set; }

    public virtual DbSet<escuela> escuelas { get; set; }

    public virtual DbSet<especializacion> especializacions { get; set; }

    public virtual DbSet<estudiante> estudiantes { get; set; }

    public virtual DbSet<evento> eventos { get; set; }

    public virtual DbSet<facultad> facultads { get; set; }

    public virtual DbSet<historial_de_cambio> historial_de_cambios { get; set; }

    public virtual DbSet<jurado> jurados { get; set; }

    public virtual DbSet<linea_investigacion> linea_investigacions { get; set; }

    public virtual DbSet<propuesta> propuestas { get; set; }

    public virtual DbSet<trabajos_de_grado> trabajos_de_grados { get; set; }

    public virtual DbSet<usuario> usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ORLANDOHP;Initial Catalog=GestionTrabajosGrado;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<asesor>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__asesor__3213E83FDE027B5D");

            entity.ToTable("asesor");

            entity.Property(e => e.disponibilidad)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.asesors)
                .HasForeignKey(d => d.id_usuario)
                .HasConstraintName("FK__asesor__id_usuar__35BCFE0A");

            entity.HasMany(d => d.id_especializacions).WithMany(p => p.id_asesors)
                .UsingEntity<Dictionary<string, object>>(
                    "asesor_especializacion",
                    r => r.HasOne<especializacion>().WithMany()
                        .HasForeignKey("id_especializacion")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__asesor_es__id_es__398D8EEE"),
                    l => l.HasOne<asesor>().WithMany()
                        .HasForeignKey("id_asesor")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__asesor_es__id_as__38996AB5"),
                    j =>
                    {
                        j.HasKey("id_asesor", "id_especializacion").HasName("PK__asesor_e__032BBB8827B05D50");
                        j.ToTable("asesor_especializacion");
                    });
        });

        modelBuilder.Entity<asesor_trabajo>(entity =>
        {
            entity.HasKey(e => new { e.id_asesor, e.id_trabajo }).HasName("PK__asesor_t__1731CA9B75417F54");

            entity.Property(e => e.rol_asesor)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.id_asesorNavigation).WithMany(p => p.asesor_trabajos)
                .HasForeignKey(d => d.id_asesor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__asesor_tr__id_as__6FE99F9F");

            entity.HasOne(d => d.id_trabajoNavigation).WithMany(p => p.asesor_trabajos)
                .HasForeignKey(d => d.id_trabajo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__asesor_tr__id_tr__70DDC3D8");
        });

        modelBuilder.Entity<director>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__director__3213E83F77F44967");

            entity.ToTable("director");

            entity.HasOne(d => d.id_escuelaNavigation).WithMany(p => p.directors)
                .HasForeignKey(d => d.id_escuela)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__director__id_esc__4316F928");

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.directors)
                .HasForeignKey(d => d.id_usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__director__id_usu__440B1D61");
        });

        modelBuilder.Entity<escuela>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__escuela__3213E83F8CEE3000");

            entity.ToTable("escuela");

            entity.Property(e => e.estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.id_facultadNavigation).WithMany(p => p.escuelas)
                .HasForeignKey(d => d.id_facultad)
                .HasConstraintName("FK__escuela__id_facu__267ABA7A");
        });

        modelBuilder.Entity<especializacion>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__especial__3213E83FD2A8E451");

            entity.ToTable("especializacion");

            entity.Property(e => e.nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<estudiante>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__estudian__3213E83F2FB2D120");

            entity.ToTable("estudiante");

            entity.HasIndex(e => e.matricula, "UQ__estudian__30962D15A64E0E26").IsUnique();

            entity.Property(e => e.matricula)
                .HasMaxLength(8)
                .IsUnicode(false);

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.estudiantes)
                .HasForeignKey(d => d.id_usuario)
                .HasConstraintName("FK__estudiant__id_us__3D5E1FD2");

            entity.HasMany(d => d.id_propuesta).WithMany(p => p.id_estudiantes)
                .UsingEntity<Dictionary<string, object>>(
                    "estudiante_propuestum",
                    r => r.HasOne<propuesta>().WithMany()
                        .HasForeignKey("id_propuesta")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__estudiant__id_pr__59FA5E80"),
                    l => l.HasOne<estudiante>().WithMany()
                        .HasForeignKey("id_estudiante")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__estudiant__id_es__59063A47"),
                    j =>
                    {
                        j.HasKey("id_estudiante", "id_propuesta").HasName("PK__estudian__AB3AE03F73FD0E51");
                        j.ToTable("estudiante_propuesta");
                    });

            entity.HasMany(d => d.id_trabajos).WithMany(p => p.id_estudiantes)
                .UsingEntity<Dictionary<string, object>>(
                    "estudiante_trabajo",
                    r => r.HasOne<trabajos_de_grado>().WithMany()
                        .HasForeignKey("id_trabajo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__estudiant__id_tr__6383C8BA"),
                    l => l.HasOne<estudiante>().WithMany()
                        .HasForeignKey("id_estudiante")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__estudiant__id_es__628FA481"),
                    j =>
                    {
                        j.HasKey("id_estudiante", "id_trabajo").HasName("PK__estudian__249A903F6930AAE6");
                        j.ToTable("estudiante_trabajo");
                    });
        });

        modelBuilder.Entity<evento>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__eventos__3213E83F529BFDFD");

            entity.Property(e => e.descripcion).HasColumnType("text");
            entity.Property(e => e.fecha).HasColumnType("datetime");
            entity.Property(e => e.titulo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.id_trabajoNavigation).WithMany(p => p.eventos)
                .HasForeignKey(d => d.id_trabajo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__eventos__id_trab__66603565");

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.eventos)
                .HasForeignKey(d => d.id_usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__eventos__id_usua__6754599E");
        });

        modelBuilder.Entity<facultad>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__facultad__3213E83F7717D40D");

            entity.ToTable("facultad");

            entity.Property(e => e.estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.nombre)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<historial_de_cambio>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__historia__3213E83F5536B92F");

            entity.Property(e => e.descripcion).HasColumnType("text");
            entity.Property(e => e.fecha).HasColumnType("datetime");
            entity.Property(e => e.titulo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.id_autorNavigation).WithMany(p => p.historial_de_cambios)
                .HasForeignKey(d => d.id_autor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__historial__id_au__6D0D32F4");

            entity.HasOne(d => d.id_propuestaNavigation).WithMany(p => p.historial_de_cambios)
                .HasForeignKey(d => d.id_propuesta)
                .HasConstraintName("FK__historial__id_pr__6C190EBB");

            entity.HasOne(d => d.id_trabajoNavigation).WithMany(p => p.historial_de_cambios)
                .HasForeignKey(d => d.id_trabajo)
                .HasConstraintName("FK__historial__id_tr__6B24EA82");
        });

        modelBuilder.Entity<jurado>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__jurado__3213E83F4EEE99A0");

            entity.ToTable("jurado");

            entity.Property(e => e.disponibilidad)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.jurados)
                .HasForeignKey(d => d.id_usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__jurado__id_usuar__4AB81AF0");

            entity.HasMany(d => d.id_trabajos).WithMany(p => p.id_jurados)
                .UsingEntity<Dictionary<string, object>>(
                    "jurados_trabajo",
                    r => r.HasOne<trabajos_de_grado>().WithMany()
                        .HasForeignKey("id_trabajo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__jurados_t__id_tr__74AE54BC"),
                    l => l.HasOne<jurado>().WithMany()
                        .HasForeignKey("id_jurado")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__jurados_t__id_ju__73BA3083"),
                    j =>
                    {
                        j.HasKey("id_jurado", "id_trabajo").HasName("PK__jurados___35CDF266FAB5383C");
                        j.ToTable("jurados_trabajos");
                    });
        });

        modelBuilder.Entity<linea_investigacion>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__linea_in__3213E83F496E2D75");

            entity.ToTable("linea_investigacion");

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.id_escuelaNavigation).WithMany(p => p.linea_investigacions)
                .HasForeignKey(d => d.id_escuela)
                .HasConstraintName("FK__linea_inv__id_es__5070F446");
        });

        modelBuilder.Entity<propuesta>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__propuest__3213E83F36348942");

            entity.Property(e => e.descripcion).HasColumnType("text");
            entity.Property(e => e.estado)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.tipo_trabajo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.titulo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.id_directorNavigation).WithMany(p => p.propuesta)
                .HasForeignKey(d => d.id_director)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__propuesta__id_di__5441852A");

            entity.HasOne(d => d.id_investigacionNavigation).WithMany(p => p.propuesta)
                .HasForeignKey(d => d.id_investigacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__propuesta__id_in__5535A963");
        });

        modelBuilder.Entity<trabajos_de_grado>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__trabajos__3213E83F26E602C0");

            entity.ToTable("trabajos_de_grado");

            entity.Property(e => e.anteproyecto)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.descripcion).HasColumnType("text");
            entity.Property(e => e.estado)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.fecha_fin).HasColumnType("datetime");
            entity.Property(e => e.fecha_inicio).HasColumnType("datetime");
            entity.Property(e => e.justificacion).HasColumnType("text");
            entity.Property(e => e.objetivo_general)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.objetivos_especificos).HasColumnType("text");
            entity.Property(e => e.planteamiento).HasColumnType("text");
            entity.Property(e => e.titulo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.trabajo)
                .HasMaxLength(2500)
                .IsUnicode(false);

            entity.HasOne(d => d.id_propuestaNavigation).WithMany(p => p.trabajos_de_grados)
                .HasForeignKey(d => d.id_propuesta)
                .HasConstraintName("FK__trabajos___id_pr__5FB337D6");
        });

        modelBuilder.Entity<usuario>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__usuario__3213E83F485A2C8A");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.correo, "UQ__usuario__2A586E0B539BDA0B").IsUnique();

            entity.Property(e => e.contraseña)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.correo)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.direccion)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.fecha_ingreso).HasColumnType("datetime");
            entity.Property(e => e.fecha_ultimo_ingreso).HasColumnType("datetime");
            entity.Property(e => e.genero)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.imagen)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.telefono)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.id_escuelaNavigation).WithMany(p => p.usuarios)
                .HasForeignKey(d => d.id_escuela)
                .HasConstraintName("FK__usuario__id_escu__2F10007B");

            entity.HasMany(d => d.id_escuelas).WithMany(p => p.id_usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "usuario_escuela",
                    r => r.HasOne<escuela>().WithMany()
                        .HasForeignKey("id_escuela")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__usuario_e__id_es__32E0915F"),
                    l => l.HasOne<usuario>().WithMany()
                        .HasForeignKey("id_usuario")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__usuario_e__id_us__31EC6D26"),
                    j =>
                    {
                        j.HasKey("id_usuario", "id_escuela").HasName("PK__usuario___81ECA796F7584B4B");
                        j.ToTable("usuario_escuelas");
                    });

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
