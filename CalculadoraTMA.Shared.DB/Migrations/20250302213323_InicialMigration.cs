using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calculadora_de_TMA.Migrations
{
    /// <inheritdoc />
    public partial class InicialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assistentes",
                columns: table => new
                {
                    AssistenteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assistentes", x => x.AssistenteId);
                });

            migrationBuilder.CreateTable(
                name: "Linhas",
                columns: table => new
                {
                    LinhaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linhas", x => x.LinhaId);
                });

            migrationBuilder.CreateTable(
                name: "Chamadas",
                columns: table => new
                {
                    ChamadaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssistenteId = table.Column<int>(type: "int", nullable: false),
                    LinhaId = table.Column<int>(type: "int", nullable: false),
                    TempoDeChamada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamadas", x => x.ChamadaId);
                    table.ForeignKey(
                        name: "FK_Chamadas_Assistentes_AssistenteId",
                        column: x => x.AssistenteId,
                        principalTable: "Assistentes",
                        principalColumn: "AssistenteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chamadas_Linhas_LinhaId",
                        column: x => x.LinhaId,
                        principalTable: "Linhas",
                        principalColumn: "LinhaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LinhasAssistentes",
                columns: table => new
                {
                    AssistenteId = table.Column<int>(type: "int", nullable: false),
                    LinhaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinhasAssistentes", x => new { x.AssistenteId, x.LinhaId });
                    table.ForeignKey(
                        name: "FK_LinhasAssistentes_Assistentes_AssistenteId",
                        column: x => x.AssistenteId,
                        principalTable: "Assistentes",
                        principalColumn: "AssistenteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinhasAssistentes_Linhas_LinhaId",
                        column: x => x.LinhaId,
                        principalTable: "Linhas",
                        principalColumn: "LinhaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chamadas_AssistenteId",
                table: "Chamadas",
                column: "AssistenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Chamadas_LinhaId",
                table: "Chamadas",
                column: "LinhaId");

            migrationBuilder.CreateIndex(
                name: "IX_LinhasAssistentes_LinhaId",
                table: "LinhasAssistentes",
                column: "LinhaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chamadas");

            migrationBuilder.DropTable(
                name: "LinhasAssistentes");

            migrationBuilder.DropTable(
                name: "Assistentes");

            migrationBuilder.DropTable(
                name: "Linhas");
        }
    }
}
