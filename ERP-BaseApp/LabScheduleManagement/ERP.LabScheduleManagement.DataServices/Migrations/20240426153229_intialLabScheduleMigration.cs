using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.LabScheduleManagement.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class intialLabScheduleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabCoordinators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CoordinatorName = table.Column<string>(type: "TEXT", nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabCoordinators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Batch = table.Column<int>(type: "INTEGER", nullable: false),
                    Specilization = table.Column<string>(type: "TEXT", nullable: false),
                    NoOfStudents = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabInstructors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    InstructorName = table.Column<string>(type: "TEXT", nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabInstructors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabSpaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SpaceName = table.Column<string>(type: "TEXT", nullable: false),
                    Floor = table.Column<string>(type: "TEXT", nullable: false),
                    TableNumber = table.Column<string>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabSpaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModuleName = table.Column<string>(type: "TEXT", nullable: false),
                    ModuleCoordinator = table.Column<string>(type: "TEXT", nullable: false),
                    NoofLabs = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    Duration = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    BookedDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    RescheduledDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    RegNo = table.Column<string>(type: "TEXT", nullable: false),
                    present = table.Column<bool>(type: "INTEGER", nullable: false),
                    LabGroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_LabGroup",
                        column: x => x.LabGroupId,
                        principalTable: "LabGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Labs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LabName = table.Column<string>(type: "TEXT", nullable: false),
                    LabDescription = table.Column<string>(type: "TEXT", nullable: false),
                    ModuleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lab_Module",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScheduledLabs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Resheduled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Completed = table.Column<bool>(type: "INTEGER", nullable: false),
                    LabInstructorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LabCoordinatorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LabId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TimeSlotId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LabSpaceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledLabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledLabs_Lab",
                        column: x => x.LabId,
                        principalTable: "Labs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ScheduledLabs_LabCoordinator",
                        column: x => x.LabCoordinatorId,
                        principalTable: "LabCoordinators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ScheduledLabs_LabInstructor",
                        column: x => x.LabInstructorId,
                        principalTable: "LabInstructors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ScheduledLabs_LabSpace",
                        column: x => x.LabSpaceId,
                        principalTable: "LabSpaces",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ScheduledLabs_TimeSlot",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LabEquipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EquipmentName = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ScheduledLabId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabEquipments_ScheduledLab",
                        column: x => x.ScheduledLabId,
                        principalTable: "ScheduledLabs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScheduledLabLabGroups",
                columns: table => new
                {
                    LabGroupsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ScheduledLabsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledLabLabGroups", x => new { x.LabGroupsId, x.ScheduledLabsId });
                    table.ForeignKey(
                        name: "FK_ScheduledLabLabGroups_LabGroups_LabGroupsId",
                        column: x => x.LabGroupsId,
                        principalTable: "LabGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledLabLabGroups_ScheduledLabs_ScheduledLabsId",
                        column: x => x.ScheduledLabsId,
                        principalTable: "ScheduledLabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabEquipments_ScheduledLabId",
                table: "LabEquipments",
                column: "ScheduledLabId");

            migrationBuilder.CreateIndex(
                name: "IX_Labs_ModuleId",
                table: "Labs",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledLabLabGroups_ScheduledLabsId",
                table: "ScheduledLabLabGroups",
                column: "ScheduledLabsId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledLabs_LabCoordinatorId",
                table: "ScheduledLabs",
                column: "LabCoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledLabs_LabId",
                table: "ScheduledLabs",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledLabs_LabInstructorId",
                table: "ScheduledLabs",
                column: "LabInstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledLabs_LabSpaceId",
                table: "ScheduledLabs",
                column: "LabSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledLabs_TimeSlotId",
                table: "ScheduledLabs",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_LabGroupId",
                table: "Students",
                column: "LabGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabEquipments");

            migrationBuilder.DropTable(
                name: "ScheduledLabLabGroups");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "ScheduledLabs");

            migrationBuilder.DropTable(
                name: "LabGroups");

            migrationBuilder.DropTable(
                name: "Labs");

            migrationBuilder.DropTable(
                name: "LabCoordinators");

            migrationBuilder.DropTable(
                name: "LabInstructors");

            migrationBuilder.DropTable(
                name: "LabSpaces");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Modules");
        }
    }
}
