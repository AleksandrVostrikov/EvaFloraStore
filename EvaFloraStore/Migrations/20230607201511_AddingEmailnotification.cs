using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaFloraStore.Migrations
{
    /// <inheritdoc />
    public partial class AddingEmailnotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmail",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmail",
                table: "Orders");
        }
    }
}
