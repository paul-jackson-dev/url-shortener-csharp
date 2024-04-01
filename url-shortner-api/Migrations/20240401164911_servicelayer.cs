using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace url_shortner_api.Migrations
{
    /// <inheritdoc />
    public partial class servicelayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrlInfo_AspNetUsers_UserId",
                table: "UrlInfo");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UrlInfo",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UrlInfo_UserId",
                table: "UrlInfo",
                newName: "IX_UrlInfo_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlInfo_AspNetUsers_AppUserId",
                table: "UrlInfo",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrlInfo_AspNetUsers_AppUserId",
                table: "UrlInfo");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "UrlInfo",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UrlInfo_AppUserId",
                table: "UrlInfo",
                newName: "IX_UrlInfo_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlInfo_AspNetUsers_UserId",
                table: "UrlInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
