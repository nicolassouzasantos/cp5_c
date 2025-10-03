using CP_05.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CP_05.Migrations;

[DbContext(typeof(ApplicationDbContext))]
partial class ApplicationDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.19");

        modelBuilder.Entity("CP_05.Domain.Entities.ClinicaEntity", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(10)")
                .HasAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            b.Property<string>("Email")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("NVARCHAR2(150)");

            b.Property<string>("Nome")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("NVARCHAR2(150)");

            b.Property<string>("Telefone")
                .HasMaxLength(30)
                .HasColumnType("NVARCHAR2(30)");

            b.HasKey("Id");

            b.ToTable("Clinicas");
        });

        modelBuilder.Entity("CP_05.Domain.Entities.EnderecoEntity", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(10)")
                .HasAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            b.Property<string>("Bairro")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("NVARCHAR2(100)");

            b.Property<string>("Cep")
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("NVARCHAR2(20)");

            b.Property<int>("ClinicaId")
                .HasColumnType("NUMBER(10)");

            b.Property<int>("Numero")
                .HasColumnType("NUMBER(10)");

            b.Property<string>("Rua")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("NVARCHAR2(150)");

            b.HasKey("Id");

            b.HasIndex("ClinicaId")
                .IsUnique();

            b.ToTable("Enderecos");
        });

        modelBuilder.Entity("CP_05.Domain.Entities.ProfissionalEntity", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(10)")
                .HasAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            b.Property<string>("Cargo")
                .HasMaxLength(100)
                .HasColumnType("NVARCHAR2(100)");

            b.Property<int>("ClinicaId")
                .HasColumnType("NUMBER(10)");

            b.Property<string>("Email")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("NVARCHAR2(150)");

            b.Property<int>("Idade")
                .HasColumnType("NUMBER(10)");

            b.Property<string>("Nome")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("NVARCHAR2(150)");

            b.HasKey("Id");

            b.HasIndex("ClinicaId");

            b.HasIndex("Email", "ClinicaId")
                .IsUnique();

            b.ToTable("Profissionais");
        });

        modelBuilder.Entity("CP_05.Domain.Entities.EnderecoEntity", b =>
        {
            b.HasOne("CP_05.Domain.Entities.ClinicaEntity", "Clinica")
                .WithOne("Endereco")
                .HasForeignKey("CP_05.Domain.Entities.EnderecoEntity", "ClinicaId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Clinica");
        });

        modelBuilder.Entity("CP_05.Domain.Entities.ProfissionalEntity", b =>
        {
            b.HasOne("CP_05.Domain.Entities.ClinicaEntity", "Clinica")
                .WithMany("Profissionais")
                .HasForeignKey("ClinicaId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Clinica");
        });

        modelBuilder.Entity("CP_05.Domain.Entities.ClinicaEntity", b =>
        {
            b.Navigation("Endereco");

            b.Navigation("Profissionais");
        });
    }
}
