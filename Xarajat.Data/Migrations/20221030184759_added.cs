using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xarajat.Data.Migrations
{
    public partial class added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "outlays",
                type: "MaxLength(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "outlays",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "MaxLength(255)",
                oldNullable: true);
        }
    }
}
