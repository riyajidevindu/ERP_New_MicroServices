using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.TrainingManagement.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class ApprovalRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("58609b03-13c8-4782-b421-0d2604926732"), null, "Coordinator", "COORDINATOR" },
                    { new Guid("7b495fc9-c12f-4076-bf4c-fad5fab56576"), null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("58609b03-13c8-4782-b421-0d2604926732"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7b495fc9-c12f-4076-bf4c-fad5fab56576"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("34c70c83-8751-4131-9315-f1986bd8c4ba"), null, "Coordinator", "COORDINATOR" },
                    { new Guid("7c8d6c78-b2b8-4f3e-95fd-0a98a6793e14"), null, "Student", "STUDENT" }
                });
        }
    }
}
