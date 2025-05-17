using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emeet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixUsertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_expert",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "total_preview",
                table: "expert",
                newName: "total_review");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "total_review",
                table: "expert",
                newName: "total_preview");

            migrationBuilder.AddColumn<bool>(
                name: "is_expert",
                table: "user",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
