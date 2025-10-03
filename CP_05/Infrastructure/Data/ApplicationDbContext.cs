using CP_05.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CP_05.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ClinicaEntity> Clinicas => Set<ClinicaEntity>();
    public DbSet<EnderecoEntity> Enderecos => Set<EnderecoEntity>();
    public DbSet<ProfissionalEntity> Profissionais => Set<ProfissionalEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ClinicaEntity>(builder =>
        {
            builder.ToTable("Clinicas");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Telefone).HasMaxLength(30);

            builder.HasOne(c => c.Endereco)
                .WithOne(e => e.Clinica)
                .HasForeignKey<EnderecoEntity>(e => e.ClinicaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Profissionais)
                .WithOne(p => p.Clinica)
                .HasForeignKey(p => p.ClinicaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<EnderecoEntity>(builder =>
        {
            builder.ToTable("Enderecos");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Rua).IsRequired().HasMaxLength(150);
            builder.Property(e => e.Numero).IsRequired();
            builder.Property(e => e.Bairro).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Cep).IsRequired().HasMaxLength(20);
        });

        modelBuilder.Entity<ProfissionalEntity>(builder =>
        {
            builder.ToTable("Profissionais");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Idade).IsRequired();
            builder.Property(p => p.Cargo).HasMaxLength(100);

            builder.HasIndex(p => new { p.Email, p.ClinicaId }).IsUnique();
        });
    }
}
