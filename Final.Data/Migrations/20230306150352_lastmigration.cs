using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final.Data.Migrations
{
    /// <inheritdoc />
    public partial class lastmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_Category_CategoryId",
                table: "Lists");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Lists_CategoryId",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Lists");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Lists",
                type: "VARCHAR(50)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Lists");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Lists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lists_CategoryId",
                table: "Lists",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_Category_CategoryId",
                table: "Lists",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
