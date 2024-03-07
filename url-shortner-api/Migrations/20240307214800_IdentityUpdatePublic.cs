using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace url_shortner_api.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUpdatePublic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "First",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "First",
                table: "AspNetUsers");
        }
    }
}
