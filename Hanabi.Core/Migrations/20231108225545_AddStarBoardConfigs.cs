using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hanabi.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddStarBoardConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "StarBoardChannel",
                table: "server_configurations",
                type: "bigint unsigned",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "StarBoardMinimalStars",
                table: "server_configurations",
                type: "int unsigned",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarBoardChannel",
                table: "server_configurations");

            migrationBuilder.DropColumn(
                name: "StarBoardMinimalStars",
                table: "server_configurations");
        }
    }
}
