using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TargetChatServer11.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_UserModel_Userid",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "Contact",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_Userid",
                table: "Contact",
                newName: "IX_Contact_Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_UserModel_Username",
                table: "Contact",
                column: "Username",
                principalTable: "UserModel",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_UserModel_Username",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Contact",
                newName: "Userid");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_Username",
                table: "Contact",
                newName: "IX_Contact_Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_UserModel_Userid",
                table: "Contact",
                column: "Userid",
                principalTable: "UserModel",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
