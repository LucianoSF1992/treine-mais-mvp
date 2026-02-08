using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TreineMais.Api.Migrations
{
    /// <inheritdoc />
    public partial class SyncDatabaseState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ExercicioConclusoes_AlunoId",
                table: "ExercicioConclusoes",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercicioConclusoes_ExercicioId",
                table: "ExercicioConclusoes",
                column: "ExercicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExercicioConclusoes_Alunos_AlunoId",
                table: "ExercicioConclusoes",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExercicioConclusoes_Exercicios_ExercicioId",
                table: "ExercicioConclusoes",
                column: "ExercicioId",
                principalTable: "Exercicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExercicioConclusoes_Alunos_AlunoId",
                table: "ExercicioConclusoes");

            migrationBuilder.DropForeignKey(
                name: "FK_ExercicioConclusoes_Exercicios_ExercicioId",
                table: "ExercicioConclusoes");

            migrationBuilder.DropIndex(
                name: "IX_ExercicioConclusoes_AlunoId",
                table: "ExercicioConclusoes");

            migrationBuilder.DropIndex(
                name: "IX_ExercicioConclusoes_ExercicioId",
                table: "ExercicioConclusoes");
        }
    }
}
