using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emeet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatetableExpert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price_per_minute",
                table: "expert");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "price_per_minute",
                table: "expert",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
