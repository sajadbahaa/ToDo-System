using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class addseedingdatataskentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "todoTasks",
                columns: table => new
                {
                    TaskID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    status = table.Column<byte>(type: "TINYINT", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todoTasks", x => x.TaskID);
                    table.ForeignKey(
                        name: "FK_todoTasks_AspNetUsers_userID",
                        column: x => x.userID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreateAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdateAt", "UserName" },
                values: new object[,]
                {
                    { "user-admin", 0, "fixed-concurrencystamp-1", new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), "admin@todo.com", true, false, null, "ADMIN@TODO.COM", "ADMIN", "AQAAAAIAAYagAAAAEFnPoIcV8horRkKxGkVEH8SrA5g93qVWQmPxfcXHfOsi0iFlZfJIh2gzDhvt3c6GnQ==", null, false, "fixed-securitystamp-1", false, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), "admin" },
                    { "user-normal", 0, "fixed-concurrencystamp-2", new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), "user@todo.com", true, false, null, "USER@TODO.COM", "USER", "AQAAAAIAAYagAAAAEIAGNE7EugpOqbWYVidEjyXBjeZjfLXwG0i/hlY+SmNF1+L4IJCKiOlSYKhd3iySuw==", null, false, "fixed-securitystamp-2", false, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), "user" }
                });

            migrationBuilder.InsertData(
                table: "todoTasks",
                columns: new[] { "TaskID", "DueDate", "createdAt", "description", "status", "title", "updatedAt", "userID" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), "مهمة تجريبية للادمن", (byte)2, "مهمة الادمن", null, "user-admin" },
                    { 2, new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), "مهمة تجريبية للمستخدم", (byte)1, "مهمة المستخدم", null, "user-normal" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_todoTasks_userID",
                table: "todoTasks",
                column: "userID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "todoTasks");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-admin");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-normal");
        }
    }
}
