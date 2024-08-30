using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.TranscriptGeneration.DataService.Migrations
{
    /// <inheritdoc />
    public partial class inti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    RegNo = table.Column<string>(type: "TEXT", nullable: false),
                    DegreeAwarded = table.Column<string>(type: "TEXT", nullable: true),
                    Specialization = table.Column<string>(type: "TEXT", nullable: true),
                    GPA = table.Column<string>(type: "TEXT", nullable: true),
                    AcademicStading = table.Column<string>(type: "TEXT", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModuleCode = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    ModuleName = table.Column<string>(type: "TEXT", nullable: false),
                    Credits = table.Column<double>(type: "REAL", nullable: false),
                    Grade = table.Column<string>(type: "TEXT", nullable: false),
                    GPV = table.Column<double>(type: "REAL", nullable: false),
                    Semester = table.Column<string>(type: "TEXT", nullable: false),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "AcademicStading", "AddedDate", "DateOfBirth", "DegreeAwarded", "FullName", "GPA", "Gender", "RegNo", "Specialization", "Status", "UpdateDate" },
                values: new object[] { new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), null, new DateTime(2024, 3, 10, 1, 46, 21, 744, DateTimeKind.Utc).AddTicks(8247), new DateTime(2024, 3, 10, 7, 16, 21, 744, DateTimeKind.Local).AddTicks(8642), null, "Gregory Hane", null, "Male", "EG/2020/4809", null, 1, new DateTime(2024, 3, 10, 1, 46, 21, 744, DateTimeKind.Utc).AddTicks(8249) });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Id", "AddedDate", "Credits", "GPV", "Grade", "ModuleCode", "ModuleName", "Semester", "Status", "StudentId", "Type", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("1e4046a9-eb1e-41bf-9c90-2c23c97ba6e8"), new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1323), 2.0, 2.2999999999999998, "C+", "EE1102", "Intoduction to Electronics", "semestr 1", 1, new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), "CM", new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1323) },
                    { new Guid("2dda597e-649c-4be1-8d11-a010f423858b"), new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1355), 2.0, 4.0, "A+", "EE1302", "Electricity", "semestr 1", 1, new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), "CM", new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1355) },
                    { new Guid("336fd8ad-5e16-4186-860d-454cef6c54a0"), new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1309), 1.0, 3.7000000000000002, "A-", "EE1101", "Intoduction to Algorithms", "semestr 1", 1, new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), "CM", new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1310) },
                    { new Guid("4b5e5553-f50c-4320-b0c5-64cb92692029"), new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1401), 2.0, 4.0, "A+", "EE1302", "Electricity", "semestr 1", 1, new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), "CM", new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1401) },
                    { new Guid("52a9c923-2c90-45e2-a541-f29d65baa01d"), new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1353), 2.0, 4.0, "A+", "EE1302", "Electricity", "semestr 1", 1, new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), "CM", new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1353) },
                    { new Guid("553f3a3f-5a3f-425c-9fff-bf32fa2657e3"), new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1326), 2.0, 3.0, "B", "EE1302", "Electricity", "semestr 1", 1, new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), "CM", new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1326) },
                    { new Guid("5b463de4-3571-4b2a-a5c2-ce63e93ba1ba"), new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1352), 2.0, 4.0, "A+", "EE1302", "Electricity", "semestr 1", 1, new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), "CM", new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1352) },
                    { new Guid("8a0c09fe-6cc9-4b20-8ba9-bb4c102ebd57"), new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1350), 2.0, 4.0, "A+", "EE1302", "Electricity", "semestr 1", 1, new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), "CM", new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1350) },
                    { new Guid("94025d0e-5b58-439b-946c-b4dd2fa97672"), new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1328), 2.0, 4.0, "A+", "EE1302", "Electricity", "semestr 1", 1, new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), "CM", new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1328) },
                    { new Guid("a1e47f7c-5d70-43a3-b033-7e4e1a8c3c25"), new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1334), 2.0, 4.0, "A+", "EE1302", "Electricity", "semestr 1", 1, new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), "CM", new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1334) },
                    { new Guid("a9c2d7e1-9e05-4e62-9045-524eb81df124"), new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1330), 2.0, 4.0, "A+", "EE1302", "Electricity", "semestr 1", 1, new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), "CM", new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1330) },
                    { new Guid("b9184b61-65a5-44ac-afaa-6283ff4f2457"), new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1349), 2.0, 4.0, "A+", "EE1302", "Electricity", "semestr 1", 1, new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), "CM", new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1349) },
                    { new Guid("fb8b101d-d636-4d8c-9047-d82464ae99ae"), new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1347), 2.0, 4.0, "A+", "EE1302", "Electricity", "semestr 1", 1, new Guid("d0fd1f61-c846-4453-8b68-71d7022f0dc2"), "CM", new DateTime(2024, 3, 10, 1, 46, 21, 745, DateTimeKind.Utc).AddTicks(1348) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_StudentId",
                table: "Results",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
