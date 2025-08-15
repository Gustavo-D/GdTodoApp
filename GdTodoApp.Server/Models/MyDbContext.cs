using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GdToDoApp.Server.Model;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tarefa> Tarefas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();
        var connectionString = configuration.GetConnectionString("EfDbConnection");

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(connectionString, optionsBuilder => optionsBuilder.CommandTimeout(900));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarefa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tarefas_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Category).HasMaxLength(384);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Description).HasMaxLength(2048);
            entity.Property(e => e.Title).HasMaxLength(384);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.User).WithMany(p => p.Tarefas)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Tarefas_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Usuario_pkey");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(84)
                .IsFixedLength();
            entity.Property(e => e.Username).HasMaxLength(256);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
