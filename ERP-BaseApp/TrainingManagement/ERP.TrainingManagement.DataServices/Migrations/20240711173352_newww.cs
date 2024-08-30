using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.TrainingManagement.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class newww : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2cc273dc-dd22-4956-aeec-edd92a41508f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eed20dff-dec5-4b08-88f9-52901b028ce7"));

            migrationBuilder.AddColumn<string>(
                name: "CompanyAdress",
                table: "ApprovalRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactedPerson",
                table: "ApprovalRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6247c1a1-36d3-4cbe-aec9-b667207a506d"), null, "Coordinator", "COORDINATOR" },
                    { new Guid("84ccf740-9d3a-4122-9258-805d7b21efd5"), null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6247c1a1-36d3-4cbe-aec9-b667207a506d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("84ccf740-9d3a-4122-9258-805d7b21efd5"));

            migrationBuilder.DropColumn(
                name: "CompanyAdress",
                table: "ApprovalRequests");

            migrationBuilder.DropColumn(
                name: "ContactedPerson",
                table: "ApprovalRequests");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2cc273dc-dd22-4956-aeec-edd92a41508f"), null, "Student", "STUDENT" },
                    { new Guid("eed20dff-dec5-4b08-88f9-52901b028ce7"), null, "Coordinator", "COORDINATOR" }
                });
        }
    }
}
