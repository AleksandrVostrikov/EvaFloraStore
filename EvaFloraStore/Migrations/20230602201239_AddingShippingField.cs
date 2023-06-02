﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaFloraStore.Migrations
{
    /// <inheritdoc />
    public partial class AddingShippingField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Shipping",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shipping",
                table: "Orders");
        }
    }
}
