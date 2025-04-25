using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class grupaRekurencyjna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                table: "Grupa",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grupa_ParentID",
                table: "Grupa",
                column: "ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grupa_Grupa_ParentID",
                table: "Grupa",
                column: "ParentID",
                principalTable: "Grupa",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grupa_Grupa_ParentID",
                table: "Grupa");

            migrationBuilder.DropIndex(
                name: "IX_Grupa_ParentID",
                table: "Grupa");

            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "Grupa");
        }
    }
}
