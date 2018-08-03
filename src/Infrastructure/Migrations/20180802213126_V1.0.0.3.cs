using Microsoft.EntityFrameworkCore.Migrations;

namespace Promociones.Infrastructure.Migrations
{
    public partial class V1003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StrMedioPagoIds",
                table: "Promociones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StrMedioPagoIds",
                table: "Promociones");
        }
    }
}
