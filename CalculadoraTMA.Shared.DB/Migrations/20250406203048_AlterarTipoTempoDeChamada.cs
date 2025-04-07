using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calculadora_de_TMA.Migrations
{
    /// <inheritdoc />
    public partial class AlterarTipoTempoDeChamada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TempoDeChamada",
                table: "Chamadas",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TempoDeChamada",
                table: "Chamadas",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
