using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SICOBack.Models;

public partial class SicoDbContext : DbContext
{
    public SicoDbContext()
    {
    }

    public SicoDbContext(DbContextOptions<SicoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCurso> TblCursos { get; set; }

    public virtual DbSet<TblCursoXestudiante> TblCursoXestudiantes { get; set; }

    public virtual DbSet<TblEstudiante> TblEstudiantes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-KDPVU52D\\SQLEXPRESS;Database=sicoDB;TrustServerCertificate=True;User Id=admin;Password=codee;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCurso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK__tblCurso__8551ED05CAA0001F");

            entity.ToTable("tblCurso");

            entity.Property(e => e.IdCurso).HasColumnName("idCurso");
            entity.Property(e => e.DtmFechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dtmFechaCreacion");
            entity.Property(e => e.StrNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("strNombre");
        });

        modelBuilder.Entity<TblCursoXestudiante>(entity =>
        {
            entity.HasKey(e => e.IdCursoXestudiante).HasName("PK__tblCurso__85AFD7A3E63BC70B");

            entity.ToTable("tblCursoXEstudiante");

            entity.Property(e => e.IdCursoXestudiante).HasColumnName("idCursoXEstudiante");
            entity.Property(e => e.IdCurso).HasColumnName("idCurso");
            entity.Property(e => e.IdEstudiante).HasColumnName("idEstudiante");
        });

        modelBuilder.Entity<TblEstudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PK__tblEstud__AEFFDBC5D9CD5199");

            entity.ToTable("tblEstudiante");

            entity.Property(e => e.IdEstudiante).HasColumnName("idEstudiante");
            entity.Property(e => e.DtmFechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dtmFechaCreacion");
            entity.Property(e => e.StrEmail)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("strEmail");
            entity.Property(e => e.StrIdentificacion)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("strIdentificacion");
            entity.Property(e => e.StrPrimerApellido)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("strPrimerApellido");
            entity.Property(e => e.StrPrimerNombre)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("strPrimerNombre");
            entity.Property(e => e.StrSegundoApellido)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("strSegundoApellido");
            entity.Property(e => e.StrSegundoNombre)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("strSegundoNombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
