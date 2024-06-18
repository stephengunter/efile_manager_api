using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationCore.Migrations
{
    /// <inheritdoc />
    public partial class Departments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Files.Judgebooks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Files.Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files.Departments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files.Judgebooks_DepartmentId",
                table: "Files.Judgebooks",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files.Judgebooks_Files.Departments_DepartmentId",
                table: "Files.Judgebooks",
                column: "DepartmentId",
                principalTable: "Files.Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files.Judgebooks_Files.Departments_DepartmentId",
                table: "Files.Judgebooks");

            migrationBuilder.DropTable(
                name: "Files.Departments");

            migrationBuilder.DropIndex(
                name: "IX_Files.Judgebooks_DepartmentId",
                table: "Files.Judgebooks");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Files.Judgebooks");
        }
    }
}
