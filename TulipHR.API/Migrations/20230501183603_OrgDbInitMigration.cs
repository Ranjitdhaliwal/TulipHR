using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TulipHR.API.Migrations
{
    public partial class OrgDbInitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    ManagerPositionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Positions_ManagerPositionId",
                        column: x => x.ManagerPositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "ManagerPositionId", "Number", "Title" },
                values: new object[] { 1, null, "1", "Director" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Number", "PositionId" },
                values: new object[] { 1, "John", "Doe", "T0001", 1 });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "ManagerPositionId", "Number", "Title" },
                values: new object[] { 2, 1, "2", "Senior Manager" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Number", "PositionId" },
                values: new object[] { 2, "Jane", "Smith", "T0002", 2 });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "ManagerPositionId", "Number", "Title" },
                values: new object[] { 3, 2, "3", "Manager 1" });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "ManagerPositionId", "Number", "Title" },
                values: new object[] { 4, 2, "3", "Manager 2" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Number", "PositionId" },
                values: new object[] { 3, "Bob", "Johnson", "T0003", 3 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Number", "PositionId" },
                values: new object[] { 4, "Dave", "Raynal", "T0004", 4 });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "ManagerPositionId", "Number", "Title" },
                values: new object[] { 5, 3, "4", "Senior Developer 1" });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "ManagerPositionId", "Number", "Title" },
                values: new object[] { 6, 4, "4", "Senior Developer 2" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Number", "PositionId" },
                values: new object[] { 5, "Michael", "Song", "T0005", 5 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Number", "PositionId" },
                values: new object[] { 6, "Brett", "Lee", "T0006", 6 });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "ManagerPositionId", "Number", "Title" },
                values: new object[] { 7, 5, "5", "Junior Developer" });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "ManagerPositionId", "Number", "Title" },
                values: new object[] { 8, 6, "5", "Junior Developer" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Number", "PositionId" },
                values: new object[] { 7, "Charles", "Smith", "T0007", 7 });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_ManagerPositionId",
                table: "Positions",
                column: "ManagerPositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
