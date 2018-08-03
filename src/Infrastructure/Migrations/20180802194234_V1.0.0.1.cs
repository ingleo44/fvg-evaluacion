using Microsoft.EntityFrameworkCore.Migrations;

namespace Promociones.Infrastructure.Migrations
{
    public partial class V1001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PorcentajeDescuento",
                table: "Promociones",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrEntidadFinancieraId",
                table: "Promociones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrProductoCategoriaIds",
                table: "Promociones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrTipoMedioPagoId",
                table: "Promociones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StrEntidadFinancieraId",
                table: "Promociones");

            migrationBuilder.DropColumn(
                name: "StrProductoCategoriaIds",
                table: "Promociones");

            migrationBuilder.DropColumn(
                name: "StrTipoMedioPagoId",
                table: "Promociones");

            migrationBuilder.AlterColumn<decimal>(
                name: "PorcentajeDescuento",
                table: "Promociones",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
