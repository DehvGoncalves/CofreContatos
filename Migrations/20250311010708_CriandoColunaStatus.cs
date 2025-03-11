using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuSiteEmMVC.Migrations
{
    public partial class CriandoColunaStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Usuarios");
        }
    }
}
