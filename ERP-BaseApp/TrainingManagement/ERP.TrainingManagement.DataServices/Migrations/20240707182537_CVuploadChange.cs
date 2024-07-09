using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.TrainingManagement.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class CVuploadChange : Migration
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
                    { new Guid("89885c30-945c-484b-bd78-49d45d92305b"), null, "Student", "STUDENT" },
                    { new Guid("dc04ab4f-ec33-43a6-a05f-1b9c4eebe869"), null, "Coordinator", "COORDINATOR" }
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
                keyValue: new Guid("89885c30-945c-484b-bd78-49d45d92305b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dc04ab4f-ec33-43a6-a05f-1b9c4eebe869"));

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
