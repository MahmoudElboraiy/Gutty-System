using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixItemName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemMeal_MainItems_ItemId",
                table: "ItemMeal");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_MainItems_ItemId",
                table: "RecipeIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainItems",
                table: "MainItems");

            migrationBuilder.RenameTable(
                name: "MainItems",
                newName: "Items");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemMeal_Items_ItemId",
                table: "ItemMeal",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Items_ItemId",
                table: "RecipeIngredients",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemMeal_Items_ItemId",
                table: "ItemMeal");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Items_ItemId",
                table: "RecipeIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "MainItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainItems",
                table: "MainItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemMeal_MainItems_ItemId",
                table: "ItemMeal",
                column: "ItemId",
                principalTable: "MainItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_MainItems_ItemId",
                table: "RecipeIngredients",
                column: "ItemId",
                principalTable: "MainItems",
                principalColumn: "Id");
        }
    }
}
