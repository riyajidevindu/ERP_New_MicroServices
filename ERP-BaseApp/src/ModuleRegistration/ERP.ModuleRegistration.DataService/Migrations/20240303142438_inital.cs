using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.ModuleRegistration.DataService.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    EmployeeNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModuleName = table.Column<string>(type: "TEXT", nullable: false),
                    ModuleCode = table.Column<string>(type: "TEXT", nullable: false),
                    Credits = table.Column<int>(type: "INTEGER", nullable: false),
                    Department = table.Column<int>(type: "INTEGER", nullable: false),
                    Curriculum = table.Column<string>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleOfferings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModuleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CoordinatorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleOfferings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleOfferings_Lecturers_CoordinatorId",
                        column: x => x.CoordinatorId,
                        principalTable: "Lecturers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AdvisorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Lecturers_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "Lecturers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModuleOfferingFirstExaminer",
                columns: table => new
                {
                    FirstExaminationsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstExaminersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleOfferingFirstExaminer", x => new { x.FirstExaminationsId, x.FirstExaminersId });
                    table.ForeignKey(
                        name: "FK_ModuleOfferingFirstExaminer_Lecturers_FirstExaminersId",
                        column: x => x.FirstExaminersId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleOfferingFirstExaminer_ModuleOfferings_FirstExaminationsId",
                        column: x => x.FirstExaminationsId,
                        principalTable: "ModuleOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleOfferingModerator",
                columns: table => new
                {
                    ModerationsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModeratorsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleOfferingModerator", x => new { x.ModerationsId, x.ModeratorsId });
                    table.ForeignKey(
                        name: "FK_ModuleOfferingModerator_Lecturers_ModeratorsId",
                        column: x => x.ModeratorsId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleOfferingModerator_ModuleOfferings_ModerationsId",
                        column: x => x.ModerationsId,
                        principalTable: "ModuleOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleOfferingSecondExaminer",
                columns: table => new
                {
                    SecondExaminationsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SecondExaminersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleOfferingSecondExaminer", x => new { x.SecondExaminationsId, x.SecondExaminersId });
                    table.ForeignKey(
                        name: "FK_ModuleOfferingSecondExaminer_Lecturers_SecondExaminersId",
                        column: x => x.SecondExaminersId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleOfferingSecondExaminer_ModuleOfferings_SecondExaminationsId",
                        column: x => x.SecondExaminationsId,
                        principalTable: "ModuleOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleOfferingStudent",
                columns: table => new
                {
                    ModulesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleOfferingStudent", x => new { x.ModulesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_ModuleOfferingStudent_ModuleOfferings_ModulesId",
                        column: x => x.ModulesId,
                        principalTable: "ModuleOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleOfferingStudent_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleOfferingFirstExaminer_FirstExaminersId",
                table: "ModuleOfferingFirstExaminer",
                column: "FirstExaminersId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleOfferingModerator_ModeratorsId",
                table: "ModuleOfferingModerator",
                column: "ModeratorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleOfferings_CoordinatorId",
                table: "ModuleOfferings",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleOfferingSecondExaminer_SecondExaminersId",
                table: "ModuleOfferingSecondExaminer",
                column: "SecondExaminersId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleOfferingStudent_StudentsId",
                table: "ModuleOfferingStudent",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AdvisorId",
                table: "Students",
                column: "AdvisorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleOfferingFirstExaminer");

            migrationBuilder.DropTable(
                name: "ModuleOfferingModerator");

            migrationBuilder.DropTable(
                name: "ModuleOfferingSecondExaminer");

            migrationBuilder.DropTable(
                name: "ModuleOfferingStudent");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "ModuleOfferings");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Lecturers");
        }
    }
}
