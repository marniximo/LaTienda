using Microsoft.EntityFrameworkCore.Migrations;

namespace LaTienda.Migrations
{
    public partial class condicionTributaria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CondicionTributaria",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CondicionTributaria",
                table: "Clientes");
        }
    }
}
