using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emeet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsertableinfullnamefiel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "full_name",
                table: "user",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "full_name",
                table: "user");
        }
    }
}
