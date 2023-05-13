using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaFloraStore.Migrations
{
    /// <inheritdoc />
    public partial class OderChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archive",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archive",
                table: "Orders");
        }
    }
}
