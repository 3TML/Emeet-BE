using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emeet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    refresh_token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    refresh_token_expiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "expert",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    experience = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    price_per_minute = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_preview = table.Column<int>(type: "int", nullable: false),
                    rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expert", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_expert",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "appointment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    expert_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    link_meet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment", x => x.id);
                    table.ForeignKey(
                        name: "FK_appointment_expert",
                        column: x => x.expert_id,
                        principalTable: "expert",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_appointment_user",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "expert_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    expert_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expert_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_expert_category_category",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_expert_category_expert",
                        column: x => x.expert_id,
                        principalTable: "expert",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "schedule",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    expert_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    day_of_month = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule", x => x.id);
                    table.ForeignKey(
                        name: "FK_schedule_expert",
                        column: x => x.expert_id,
                        principalTable: "expert",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "static_file",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    type = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    expert_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_static_file", x => x.id);
                    table.ForeignKey(
                        name: "FK_static_file_expert",
                        column: x => x.expert_id,
                        principalTable: "expert",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "feedback",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpertId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rate = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedback", x => x.id);
                    table.ForeignKey(
                        name: "FK_feedback_appointment",
                        column: x => x.AppointmentId,
                        principalTable: "appointment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_feedback_expert",
                        column: x => x.ExpertId,
                        principalTable: "expert",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    appointment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    payment_method = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    transaction_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date_paid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_payment_appointment",
                        column: x => x.appointment_id,
                        principalTable: "appointment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointment_expert_id",
                table: "appointment",
                column: "expert_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_user_id",
                table: "appointment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_expert_user_id",
                table: "expert",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_expert_category_category_id",
                table: "expert_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_expert_category_expert_id",
                table: "expert_category",
                column: "expert_id");

            migrationBuilder.CreateIndex(
                name: "IX_feedback_AppointmentId",
                table: "feedback",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_feedback_ExpertId",
                table: "feedback",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_appointment_id",
                table: "payment",
                column: "appointment_id");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_expert_id",
                table: "schedule",
                column: "expert_id");

            migrationBuilder.CreateIndex(
                name: "IX_static_file_expert_id",
                table: "static_file",
                column: "expert_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expert_category");

            migrationBuilder.DropTable(
                name: "feedback");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "schedule");

            migrationBuilder.DropTable(
                name: "static_file");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "appointment");

            migrationBuilder.DropTable(
                name: "expert");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
