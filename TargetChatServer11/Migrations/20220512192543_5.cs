using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TargetChatServer11.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_UserModel_Username",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Contact_Contactid",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_Contactid",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Contactid",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Contact",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_Username",
                table: "Contact",
                newName: "IX_Contact_UserId");

            migrationBuilder.AddColumn<int>(
                name: "ContactIdentifier",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Identifier",
                table: "Contact",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                column: "Identifier");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ContactIdentifier",
                table: "Message",
                column: "ContactIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_UserModel_UserId",
                table: "Contact",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Contact_ContactIdentifier",
                table: "Message",
                column: "ContactIdentifier",
                principalTable: "Contact",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_UserModel_UserId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Contact_ContactIdentifier",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ContactIdentifier",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ContactIdentifier",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Contact",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_Contact_UserId",
                table: "Contact",
                newName: "IX_Contact_Username");

            migrationBuilder.AddColumn<string>(
                name: "Contactid",
                table: "Message",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "Contact",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                table: "Contact",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Message_Contactid",
                table: "Message",
                column: "Contactid");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_UserModel_Username",
                table: "Contact",
                column: "Username",
                principalTable: "UserModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Contact_Contactid",
                table: "Message",
                column: "Contactid",
                principalTable: "Contact",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
