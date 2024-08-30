using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.Authentication.DataService.Migrations
{
    /// <inheritdoc />
    public partial class intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "Id", "AddedDate", "Password", "Role", "Status", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("87139f4a-33f5-4b7b-907a-1f63776e2244"), new DateTime(2024, 3, 14, 23, 14, 17, 523, DateTimeKind.Utc).AddTicks(8967), "user", "User", 0, new DateTime(2024, 3, 14, 23, 14, 17, 523, DateTimeKind.Utc).AddTicks(8967), "user" },
                    { new Guid("aacf9a50-e18f-41e8-bbf2-7c2c2d05c32b"), new DateTime(2024, 3, 14, 23, 14, 17, 523, DateTimeKind.Utc).AddTicks(8962), "admin", "Adminstrator", 0, new DateTime(2024, 3, 14, 23, 14, 17, 523, DateTimeKind.Utc).AddTicks(8964), "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccounts");
        }
    }
}
