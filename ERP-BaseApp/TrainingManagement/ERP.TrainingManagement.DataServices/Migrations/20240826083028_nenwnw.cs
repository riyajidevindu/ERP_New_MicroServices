using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.TrainingManagement.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class nenwnw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6247c1a1-36d3-4cbe-aec9-b667207a506d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("84ccf740-9d3a-4122-9258-805d7b21efd5"));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CVUploads",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("437be862-4af7-4208-b5a5-437f441d2716"), null, "Student", "STUDENT" },
                    { new Guid("d7025e37-3220-477d-9003-d67008093c58"), null, "Coordinator", "COORDINATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("437be862-4af7-4208-b5a5-437f441d2716"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d7025e37-3220-477d-9003-d67008093c58"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CVUploads");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6247c1a1-36d3-4cbe-aec9-b667207a506d"), null, "Coordinator", "COORDINATOR" },
                    { new Guid("84ccf740-9d3a-4122-9258-805d7b21efd5"), null, "Student", "STUDENT" }
                });
        }
    }
}
