using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.TrainingManagement.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class CvUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("58609b03-13c8-4782-b421-0d2604926732"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7b495fc9-c12f-4076-bf4c-fad5fab56576"));

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "CVUploads",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "CVUploads",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "CVUploads",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "RegistrationLetterUploads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FileData = table.Column<byte[]>(type: "BLOB", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationLetterUploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationLetterUploads_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("62be87d3-9b4f-4e43-89ca-3d3600d15bc0"), null, "Coordinator", "COORDINATOR" },
                    { new Guid("89e97275-13f7-49bd-9ca2-83332d05827a"), null, "Student", "STUDENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationLetterUploads_StudentId",
                table: "RegistrationLetterUploads",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationLetterUploads");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("62be87d3-9b4f-4e43-89ca-3d3600d15bc0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("89e97275-13f7-49bd-9ca2-83332d05827a"));

            migrationBuilder.DropColumn(
                name: "FileData",
                table: "CVUploads");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "CVUploads");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "CVUploads");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("58609b03-13c8-4782-b421-0d2604926732"), null, "Coordinator", "COORDINATOR" },
                    { new Guid("7b495fc9-c12f-4076-bf4c-fad5fab56576"), null, "Student", "STUDENT" }
                });
        }
    }
}
