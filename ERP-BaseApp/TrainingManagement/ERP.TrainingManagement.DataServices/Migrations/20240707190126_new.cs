using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.TrainingManagement.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("62be87d3-9b4f-4e43-89ca-3d3600d15bc0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("89e97275-13f7-49bd-9ca2-83332d05827a"));

            migrationBuilder.AddColumn<Guid>(
                name: "VacancyId",
                table: "CVUploads",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2cc273dc-dd22-4956-aeec-edd92a41508f"), null, "Student", "STUDENT" },
                    { new Guid("eed20dff-dec5-4b08-88f9-52901b028ce7"), null, "Coordinator", "COORDINATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CVUploads_VacancyId",
                table: "CVUploads",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CVUploads_InternshipVacancies_VacancyId",
                table: "CVUploads",
                column: "VacancyId",
                principalTable: "InternshipVacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CVUploads_InternshipVacancies_VacancyId",
                table: "CVUploads");

            migrationBuilder.DropIndex(
                name: "IX_CVUploads_VacancyId",
                table: "CVUploads");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2cc273dc-dd22-4956-aeec-edd92a41508f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eed20dff-dec5-4b08-88f9-52901b028ce7"));

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "CVUploads");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("62be87d3-9b4f-4e43-89ca-3d3600d15bc0"), null, "Coordinator", "COORDINATOR" },
                    { new Guid("89e97275-13f7-49bd-9ca2-83332d05827a"), null, "Student", "STUDENT" }
                });
        }
    }
}
