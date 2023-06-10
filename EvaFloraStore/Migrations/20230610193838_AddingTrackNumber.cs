using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaFloraStore.Migrations
{
    /// <inheritdoc />
    public partial class AddingTrackNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Track",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Track",
                table: "Orders");
        }
    }
}
