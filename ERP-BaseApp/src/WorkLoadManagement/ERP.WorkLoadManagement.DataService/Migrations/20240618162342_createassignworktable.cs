using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.WorkLoadManagement.DataService.Migrations
{
    /// <inheritdoc />
    public partial class createassignworktable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    WorkId = table.Column<Guid>(type: "TEXT", nullable: true),
                    StaffId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Duration = table.Column<string>(type: "TEXT", nullable: false),
                    IsRejected = table.Column<bool>(type: "INTEGER", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AssignByUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignWorks_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignWorks_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignWorks_StaffId",
                table: "AssignWorks",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignWorks_WorkId",
                table: "AssignWorks",
                column: "WorkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignWorks");
        }
    }
}
