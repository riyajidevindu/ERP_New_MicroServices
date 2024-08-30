using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.TrainingManagement.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class UserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1794ac11-2d04-49e3-8769-49b59fa05953"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2d7943d4-dbfc-422a-8c4e-c4ca669a9657"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("56cfea4a-d15b-43de-8d3f-19b08027ccf4"), null, "Coordinator", "COORDINATOR" },
                    { new Guid("c7327a28-f491-4f5b-bac6-0865c1fc54db"), null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("56cfea4a-d15b-43de-8d3f-19b08027ccf4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c7327a28-f491-4f5b-bac6-0865c1fc54db"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1794ac11-2d04-49e3-8769-49b59fa05953"), null, "Student", "STUDENT" },
                    { new Guid("2d7943d4-dbfc-422a-8c4e-c4ca669a9657"), null, "Coordinator", "COORDINATOR" }
                });
        }
    }
}
