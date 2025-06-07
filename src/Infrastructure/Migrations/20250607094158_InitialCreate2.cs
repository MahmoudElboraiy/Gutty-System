using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_ExtraItemOptions_ExtraItemOptionId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_ExtraItemOptionId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "ExtraItemOptionId",
                table: "Meals");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Meals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "Meals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WeightToPriceRatio",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "WeightToPriceRatio",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "ExtraItemOptionId",
                table: "Meals",
                type: "int",
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
        }
    }
}
