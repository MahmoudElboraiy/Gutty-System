using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_SideItems_SideItemId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MainItems_MainItemId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_MainItems_MainItemId",
                table: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "MealSideItem");

            migrationBuilder.DropTable(
                name: "SideItems");

            migrationBuilder.DropIndex(
                name: "IX_Meals_MainItemId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_SideItemId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "MainItemId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "SideItemId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "MainItemId",
                table: "RecipeIngredients",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_MainItemId",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_ItemId");

            migrationBuilder.AddColumn<bool>(
                name: "IsMainItem",
                table: "MainItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ItemMeal",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MealsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMeal", x => new { x.ItemId, x.MealsId });
                    table.ForeignKey(
                        name: "FK_ItemMeal_MainItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "MainItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemMeal_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemMeal_MealsId",
                table: "ItemMeal",
                column: "MealsId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_MainItems_ItemId",
                table: "RecipeIngredients",
                column: "ItemId",
                principalTable: "MainItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_MainItems_ItemId",
                table: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "ItemMeal");

            migrationBuilder.DropColumn(
                name: "IsMainItem",
                table: "MainItems");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "RecipeIngredients",
                newName: "MainItemId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredients_ItemId",
                table: "RecipeIngredients",
                newName: "IX_RecipeIngredients_MainItemId");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "RecipeIngredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "MainItemId",
                table: "Meals",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SideItemId",
                table: "Ingredients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SideItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Calories = table.Column<decimal>(type: "TEXT", nullable: true),
                    Carbohydrates = table.Column<decimal>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Fats = table.Column<decimal>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Proteins = table.Column<decimal>(type: "TEXT", nullable: false),
                    Weight = table.Column<decimal>(type: "TEXT", nullable: true),
                    WeightRaw = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SideItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealSideItem",
                columns: table => new
                {
                    MealsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SideDishesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealSideItem", x => new { x.MealsId, x.SideDishesId });
                    table.ForeignKey(
                        name: "FK_MealSideItem_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealSideItem_SideItems_SideDishesId",
                        column: x => x.SideDishesId,
                        principalTable: "SideItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_MainItemId",
                table: "Meals",
                column: "MainItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_SideItemId",
                table: "Ingredients",
                column: "SideItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MealSideItem_SideDishesId",
                table: "MealSideItem",
                column: "SideDishesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_SideItems_SideItemId",
                table: "Ingredients",
                column: "SideItemId",
                principalTable: "SideItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MainItems_MainItemId",
                table: "Meals",
                column: "MainItemId",
                principalTable: "MainItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_MainItems_MainItemId",
                table: "RecipeIngredients",
                column: "MainItemId",
                principalTable: "MainItems",
                principalColumn: "Id");
        }
    }
}
