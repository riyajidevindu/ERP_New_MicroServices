using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.TrainingManagement.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class nenwnwmnj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("437be862-4af7-4208-b5a5-437f441d2716"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d7025e37-3220-477d-9003-d67008093c58"));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ApprovalRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("608ba4be-92e0-41c6-986a-75004b07f070"), null, "Student", "STUDENT" },
                    { new Guid("c4cff93b-f631-4bc7-aed2-407b9cddedf2"), null, "Coordinator", "COORDINATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("608ba4be-92e0-41c6-986a-75004b07f070"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c4cff93b-f631-4bc7-aed2-407b9cddedf2"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ApprovalRequests");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("437be862-4af7-4208-b5a5-437f441d2716"), null, "Student", "STUDENT" },
                    { new Guid("d7025e37-3220-477d-9003-d67008093c58"), null, "Coordinator", "COORDINATOR" }
                });
        }
    }
}
