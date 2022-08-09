using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceApp.Api.Migrations
{
    public partial class CampoBaixadaDespesa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Baixada",
                table: "Despesa",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Baixada",
                table: "Despesa");
        }
    }
}
