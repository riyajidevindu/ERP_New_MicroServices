using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.TrainingManagement.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class UserSeedNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("34c70c83-8751-4131-9315-f1986bd8c4ba"), null, "Coordinator", "COORDINATOR" },
                    { new Guid("7c8d6c78-b2b8-4f3e-95fd-0a98a6793e14"), null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("34c70c83-8751-4131-9315-f1986bd8c4ba"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7c8d6c78-b2b8-4f3e-95fd-0a98a6793e14"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("56cfea4a-d15b-43de-8d3f-19b08027ccf4"), null, "Coordinator", "COORDINATOR" },
                    { new Guid("c7327a28-f491-4f5b-bac6-0865c1fc54db"), null, "Student", "STUDENT" }
                });
        }
    }
}
