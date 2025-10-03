using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CP_05.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Clinicas",
            columns: table => new
            {
                Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                    .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                Nome = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                Email = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                Telefone = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Clinicas", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Enderecos",
            columns: table => new
            {
                Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                    .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                Rua = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                Numero = table.Column<int>(type: "NUMBER(10)", nullable: false),
                Bairro = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                Cep = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                ClinicaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Enderecos", x => x.Id);
                table.ForeignKey(
                    name: "FK_Enderecos_Clinicas_ClinicaId",
                    column: x => x.ClinicaId,
                    principalTable: "Clinicas",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Profissionais",
            columns: table => new
            {
                Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                    .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                Nome = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                Email = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                Idade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                Cargo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                ClinicaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Profissionais", x => x.Id);
                table.ForeignKey(
                    name: "FK_Profissionais_Clinicas_ClinicaId",
                    column: x => x.ClinicaId,
                    principalTable: "Clinicas",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Enderecos_ClinicaId",
            table: "Enderecos",
            column: "ClinicaId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Profissionais_ClinicaId",
            table: "Profissionais",
            column: "ClinicaId");

        migrationBuilder.CreateIndex(
            name: "IX_Profissionais_Email_ClinicaId",
            table: "Profissionais",
            columns: new[] { "Email", "ClinicaId" },
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Enderecos");

        migrationBuilder.DropTable(
            name: "Profissionais");

        migrationBuilder.DropTable(
            name: "Clinicas");
    }
}
