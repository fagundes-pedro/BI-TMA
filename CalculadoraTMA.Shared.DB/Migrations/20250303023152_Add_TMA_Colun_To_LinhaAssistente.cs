using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calculadora_de_TMA.Migrations
{
    /// <inheritdoc />
    public partial class Add_TMA_Colun_To_LinhaAssistente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TMA",
                table: "LinhasAssistentes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TMA",
                table: "LinhasAssistentes");
        }
    }
}
