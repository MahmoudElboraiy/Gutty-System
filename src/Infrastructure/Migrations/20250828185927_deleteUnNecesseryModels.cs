using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deleteUnNecesseryModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraItemOptions");

            migrationBuilder.DropTable(
                name: "IngredientChanges");

            migrationBuilder.DropTable(
                name: "ItemIngredients");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StockQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserPrefernceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_UserPrefernces_UserPrefernceId",
                        column: x => x.UserPrefernceId,
                        principalTable: "UserPrefernces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Calories = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Carbs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fats = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fibers = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HasCarb = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MenuType = table.Column<int>(type: "int", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proteins = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UserPrefernceId = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeightToPriceRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_UserPrefernces_UserPrefernceId",
                        column: x => x.UserPrefernceId,
                        principalTable: "UserPrefernces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IngredientChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NewValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OldValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientChanges_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtraItemOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraItemOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraItemOptions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsOptional = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemIngredients_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MealType = table.Column<int>(type: "int", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraItemOptions_ItemId",
                table: "ExtraItemOptions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientChanges_IngredientId",
                table: "IngredientChanges",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_UserPrefernceId",
                table: "Ingredients",
                column: "UserPrefernceId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemIngredients_IngredientId",
                table: "ItemIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemIngredients_ItemId",
                table: "ItemIngredients",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserPrefernceId",
                table: "Items",
                column: "UserPrefernceId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_ItemId",
                table: "Meals",
                column: "ItemId");
        }
    }
}
