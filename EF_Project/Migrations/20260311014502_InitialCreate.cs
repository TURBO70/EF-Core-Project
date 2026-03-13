using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EF_Project.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentID = table.Column<int>(name: "Department ID", type: "int", nullable: false),
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Course_Student",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course_Student", x => new { x.CourseID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_Course_Student_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Course_Student_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseSession",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseSession_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseSessionAttendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseSessionId = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSessionAttendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseSessionAttendance_CourseSession_CourseSessionId",
                        column: x => x.CourseSessionId,
                        principalTable: "CourseSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSessionAttendance_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(name: "Department ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentID = table.Column<int>(name: "Department ID", type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Instructor_Department_Department ID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "Department ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Department ID", "Location", "ManagerID", "Name" },
                values: new object[,]
                {
                    { 1, "Building A", null, "Computer Science" },
                    { 2, "Building B", null, "Mathematics" },
                    { 3, "Building C", null, "Physics" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "ID", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "Alice", "Williams", "555-1001" },
                    { 2, "Bob", "Davis", "555-1002" },
                    { 3, "Charlie", "Miller", "555-1003" },
                    { 4, "Diana", "Wilson", "555-1004" },
                    { 5, "Edward", "Moore", "555-1005" }
                });

            migrationBuilder.InsertData(
                table: "Instructor",
                columns: new[] { "ID", "Department ID", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, 1, "John", "Smith", "555-0101" },
                    { 2, 1, "Jane", "Doe", "555-0102" },
                    { 3, 2, "Robert", "Johnson", "555-0103" },
                    { 4, 3, "Emily", "Brown", "555-0104" }
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "ID", "Department ID", "Duration", "InstructorID", "Name" },
                values: new object[,]
                {
                    { 1, 1, 60, 1, "Introduction to Programming" },
                    { 2, 1, 90, 2, "Data Structures" },
                    { 3, 2, 45, 3, "Calculus I" },
                    { 4, 3, 60, 4, "Physics 101" }
                });

            migrationBuilder.InsertData(
                table: "CourseSession",
                columns: new[] { "Id", "CourseID", "Date", "InstructorID", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Variables and Types" },
                    { 2, 1, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Control Flow" },
                    { 3, 2, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Arrays and Lists" },
                    { 4, 3, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Limits" },
                    { 5, 4, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Newton's Laws" }
                });

            migrationBuilder.InsertData(
                table: "Course_Student",
                columns: new[] { "CourseID", "StudentID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 4 },
                    { 3, 2 },
                    { 3, 5 },
                    { 4, 3 },
                    { 4, 4 },
                    { 4, 5 }
                });

            migrationBuilder.InsertData(
                table: "CourseSessionAttendance",
                columns: new[] { "Id", "CourseSessionId", "Grade", "Notes", "StudentID" },
                values: new object[,]
                {
                    { 1, 1, 95, "Excellent participation", 1 },
                    { 2, 1, 88, "Good work", 2 },
                    { 3, 2, 92, null, 1 },
                    { 4, 3, 85, "Needs improvement", 1 },
                    { 5, 4, 90, null, 2 },
                    { 6, 5, 78, "Late submission", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_Department ID",
                table: "Course",
                column: "Department ID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_InstructorID",
                table: "Course",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Student_StudentID",
                table: "Course_Student",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSession_CourseID",
                table: "CourseSession",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSession_InstructorID",
                table: "CourseSession",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSessionAttendance_CourseSessionId",
                table: "CourseSessionAttendance",
                column: "CourseSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSessionAttendance_StudentID",
                table: "CourseSessionAttendance",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ManagerID",
                table: "Department",
                column: "ManagerID",
                unique: true,
                filter: "[ManagerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_Department ID",
                table: "Instructor",
                column: "Department ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Department_Department ID",
                table: "Course",
                column: "Department ID",
                principalTable: "Department",
                principalColumn: "Department ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Instructor_InstructorID",
                table: "Course",
                column: "InstructorID",
                principalTable: "Instructor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSession_Instructor_InstructorID",
                table: "CourseSession",
                column: "InstructorID",
                principalTable: "Instructor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Instructor_ManagerID",
                table: "Department",
                column: "ManagerID",
                principalTable: "Instructor",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Department_Department ID",
                table: "Instructor");

            migrationBuilder.DropTable(
                name: "Course_Student");

            migrationBuilder.DropTable(
                name: "CourseSessionAttendance");

            migrationBuilder.DropTable(
                name: "CourseSession");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Instructor");
        }
    }
}
