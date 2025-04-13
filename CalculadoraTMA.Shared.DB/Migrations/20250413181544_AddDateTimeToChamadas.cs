using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calculadora_de_TMA.Migrations
{
    /// <inheritdoc />
    public partial class AddDateTimeToChamadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataHora",
                table: "Chamadas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataHora",
                table: "Chamadas");
        }
    }
}
