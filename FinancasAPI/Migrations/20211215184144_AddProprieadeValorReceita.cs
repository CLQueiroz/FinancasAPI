using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceApp.Api.Migrations
{
    public partial class AddProprieadeValorReceita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receita_Usuario_UsuarioId",
                table: "Receita");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Receita",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Receita",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Receita_Usuario_UsuarioId",
                table: "Receita",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receita_Usuario_UsuarioId",
                table: "Receita");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Receita");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Receita",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Receita_Usuario_UsuarioId",
                table: "Receita",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
