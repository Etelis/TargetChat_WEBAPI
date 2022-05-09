using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace targetchatserver.Migrations
{
    public partial class inits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Contact_ContactId",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                table: "Message",
                newName: "Contactid");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ContactId",
                table: "Message",
                newName: "IX_Message_Contactid");

            migrationBuilder.RenameColumn(
                name: "Server",
                table: "Contact",
                newName: "server");

            migrationBuilder.RenameColumn(
                name: "LastDate",
                table: "Contact",
                newName: "lastdate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Contact",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LastMessage",
                table: "Contact",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ContactName",
                table: "Contact",
                newName: "last");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Contact_Contactid",
                table: "Message",
                column: "Contactid",
                principalTable: "Contact",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Contact_Contactid",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "Contactid",
                table: "Message",
                newName: "ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_Contactid",
                table: "Message",
                newName: "IX_Message_ContactId");

            migrationBuilder.RenameColumn(
                name: "server",
                table: "Contact",
                newName: "Server");

            migrationBuilder.RenameColumn(
                name: "lastdate",
                table: "Contact",
                newName: "LastDate");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Contact",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Contact",
                newName: "LastMessage");

            migrationBuilder.RenameColumn(
                name: "last",
                table: "Contact",
                newName: "ContactName");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Contact_ContactId",
                table: "Message",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
