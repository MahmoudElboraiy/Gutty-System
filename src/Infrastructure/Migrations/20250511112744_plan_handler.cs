using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class plan_handler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Items_ItemId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "TotalCalories",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "TotalCarbs",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "TotalFats",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "TotalProteins",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "TotalWeight",
                table: "Meals");

            migrationBuilder.AlterColumn<Guid>(
                name: "ItemId",
                table: "Meals",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExtraItemOptionId",
                table: "Meals",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_ExtraItemOptionId",
                table: "Meals",
                column: "ExtraItemOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_ExtraItemOptions_ExtraItemOptionId",
                table: "Meals",
                column: "ExtraItemOptionId",
                principalTable: "ExtraItemOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Items_ItemId",
                table: "Meals",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_ExtraItemOptions_ExtraItemOptionId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Items_ItemId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_ExtraItemOptionId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "ExtraItemOptionId",
                table: "Meals");

            migrationBuilder.AlterColumn<Guid>(
                name: "ItemId",
                table: "Meals",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCalories",
                table: "Meals",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCarbs",
                table: "Meals",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalFats",
                table: "Meals",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Meals",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalProteins",
                table: "Meals",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalWeight",
                table: "Meals",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Items_ItemId",
                table: "Meals",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
