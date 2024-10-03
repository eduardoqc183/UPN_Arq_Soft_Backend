using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.Models;

public partial class JorplastContext : DbContext
{
    public JorplastContext()
    {
    }

    public JorplastContext(DbContextOptions<JorplastContext> options)
        : base(options)
    {
    }

    public virtual DbSet<carritocompra> carritocompras { get; set; }

    public virtual DbSet<cliente> clientes { get; set; }

    public virtual DbSet<pago> pagos { get; set; }

    public virtual DbSet<persona> personas { get; set; }

    public virtual DbSet<producto> productos { get; set; }

    public virtual DbSet<usuario> usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Jorplast_UPN;Trusted_Connection=true;TrustServerCertificate=True;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<carritocompra>(entity =>
        {
            entity.ToTable("carritocompra");

            entity.HasIndex(e => e.usuarioid, "IX_Relationship5");

            entity.HasIndex(e => e.productoid, "IX_Relationship6");

            entity.HasIndex(e => e.pagoid, "IX_Relationship7");

            entity.Property(e => e.fecharegistro).HasColumnType("datetime");

            entity.HasOne(d => d.pago).WithMany(p => p.carritocompras)
                .HasForeignKey(d => d.pagoid)
                .HasConstraintName("fk_pago_carritocompra");

            entity.HasOne(d => d.producto).WithMany(p => p.carritocompras)
                .HasForeignKey(d => d.productoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_producto_carritocompra");

            entity.HasOne(d => d.usuario).WithMany(p => p.carritocompras)
                .HasForeignKey(d => d.usuarioid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_carritocompra");
        });

        modelBuilder.Entity<cliente>(entity =>
        {
            entity.ToTable("cliente");

            entity.HasIndex(e => e.personaid, "IX_Relationship2");

            entity.Property(e => e.correoalternativo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.departamento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.direccion).IsUnicode(false);
            entity.Property(e => e.fecharegistro).HasColumnType("datetime");
            entity.Property(e => e.nrocelular)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.provincia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.referencia).IsUnicode(false);

            entity.HasOne(d => d.persona).WithMany(p => p.clientes)
                .HasForeignKey(d => d.personaid)
                .HasConstraintName("fk_persona_cliente");
        });

        modelBuilder.Entity<pago>(entity =>
        {
            entity.ToTable("pago");

            entity.Property(e => e.pagoid).ValueGeneratedNever();
            entity.Property(e => e.fecharegistro)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.igv).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.total).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.valortotal).HasColumnType("numeric(18, 2)");
        });

        modelBuilder.Entity<persona>(entity =>
        {
            entity.ToTable("persona");

            entity.Property(e => e.apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.fecharegistro).HasColumnType("datetime");
            entity.Property(e => e.nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.nrodocidentidad)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<producto>(entity =>
        {
            entity.ToTable("producto");

            entity.Property(e => e.codigoproducto)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.fecharegistro).HasColumnType("datetime");
            entity.Property(e => e.imagenurl).IsUnicode(false);
            entity.Property(e => e.marca)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.nombre)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.preciosinigv).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.vendedor)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<usuario>(entity =>
        {
            entity.ToTable("usuario");

            entity.HasIndex(e => e.personaid, "IX_Relationship1");

            entity.Property(e => e.email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.fecharegistro).HasColumnType("datetime");
            entity.Property(e => e.password).IsUnicode(false);

            entity.HasOne(d => d.persona).WithMany(p => p.usuarios)
                .HasForeignKey(d => d.personaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_persona_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
