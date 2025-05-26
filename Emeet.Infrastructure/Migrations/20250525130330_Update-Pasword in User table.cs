using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emeet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaswordinUsertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Service",
                table: "Service");

            migrationBuilder.RenameTable(
                name: "Service",
                newName: "ex_service");

            migrationBuilder.RenameColumn(
                name: "day_of_month",
                table: "schedule",
                newName: "day_of_week");

            migrationBuilder.RenameIndex(
                name: "IX_Service_expert_id",
                table: "ex_service",
                newName: "IX_ex_service_expert_id");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "user",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<Guid>(
                name: "service_id",
                table: "appointment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "time",
                table: "ex_service",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "ex_service",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ex_service",
                table: "ex_service",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_service_id",
                table: "appointment",
                column: "service_id");

            migrationBuilder.AddForeignKey(
                name: "FK_appointment_exservice",
                table: "appointment",
                column: "service_id",
                principalTable: "ex_service",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointment_exservice",
                table: "appointment");

            migrationBuilder.DropIndex(
                name: "IX_appointment_service_id",
                table: "appointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ex_service",
                table: "ex_service");

            migrationBuilder.DropColumn(
                name: "service_id",
                table: "appointment");

            migrationBuilder.RenameTable(
                name: "ex_service",
                newName: "Service");

            migrationBuilder.RenameColumn(
                name: "day_of_week",
                table: "schedule",
                newName: "day_of_month");

            migrationBuilder.RenameIndex(
                name: "IX_ex_service_expert_id",
                table: "Service",
                newName: "IX_Service_expert_id");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "user",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<decimal>(
                name: "time",
                table: "Service",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Service",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service",
                table: "Service",
                column: "Id");
        }
    }
}
