using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace targetchatserver.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Contact",
                newName: "ContactName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactName",
                table: "Contact",
                newName: "UserName");
        }
    }
}
