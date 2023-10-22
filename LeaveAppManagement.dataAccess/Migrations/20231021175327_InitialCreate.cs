using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveAppManagement.dataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TLeaveCalendars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLeaveCalendars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TManager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TManager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TAdmin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAdmin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TAdmin_TUsers_Id",
                        column: x => x.Id,
                        principalTable: "TUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TEmployee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LeaveBalanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TEmployee_TUsers_Id",
                        column: x => x.Id,
                        principalTable: "TUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TLeaveBalance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotaLeaveAvailable = table.Column<int>(type: "int", nullable: false),
                    TotalCurrentLeave = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLeaveBalance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TLeaveBalance_TEmployee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "TEmployee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TLeaveRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateRequest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveCalendarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLeaveRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TLeaveRequest_TEmployee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "TEmployee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TLeaveRequest_TLeaveCalendars_LeaveCalendarId",
                        column: x => x.LeaveCalendarId,
                        principalTable: "TLeaveCalendars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TLeaveRequest_TManager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "TManager",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TLeaveBalance_EmployeeId",
                table: "TLeaveBalance",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TLeaveRequest_EmployeeId",
                table: "TLeaveRequest",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TLeaveRequest_LeaveCalendarId",
                table: "TLeaveRequest",
                column: "LeaveCalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_TLeaveRequest_ManagerId",
                table: "TLeaveRequest",
                column: "ManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAdmin");

            migrationBuilder.DropTable(
                name: "TLeaveBalance");

            migrationBuilder.DropTable(
                name: "TLeaveRequest");

            migrationBuilder.DropTable(
                name: "TEmployee");

            migrationBuilder.DropTable(
                name: "TLeaveCalendars");

            migrationBuilder.DropTable(
                name: "TManager");

            migrationBuilder.DropTable(
                name: "TUsers");
        }
    }
}
