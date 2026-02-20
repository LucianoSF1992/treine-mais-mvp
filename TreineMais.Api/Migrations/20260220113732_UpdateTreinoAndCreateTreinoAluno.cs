using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TreineMais.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTreinoAndCreateTreinoAluno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Treinos",
                table: "Treinos");

            migrationBuilder.RenameTable(
                name: "Treinos",
                newName: "Treino");

            migrationBuilder.RenameColumn(
                name: "DiaSemana",
                table: "Treino",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "AlunoId",
                table: "Treino",
                newName: "InstrutorId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Treino",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Treino",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Treino",
                table: "Treino",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TreinoAlunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TreinoId = table.Column<int>(type: "int", nullable: false),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    DiaSemana = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreinoAlunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreinoAlunos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreinoAlunos_Treino_TreinoId",
                        column: x => x.TreinoId,
                        principalTable: "Treino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TreinoAlunos_AlunoId",
                table: "TreinoAlunos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_TreinoAlunos_TreinoId",
                table: "TreinoAlunos",
                column: "TreinoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreinoAlunos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Treino",
                table: "Treino");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Treino");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Treino");

            migrationBuilder.RenameTable(
                name: "Treino",
                newName: "Treinos");

            migrationBuilder.RenameColumn(
                name: "InstrutorId",
                table: "Treinos",
                newName: "AlunoId");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Treinos",
                newName: "DiaSemana");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Treinos",
                table: "Treinos",
                column: "Id");
        }
    }
}
