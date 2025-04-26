using System;
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
            migrationBuilder.DropTable(
                name: "IngredientLogs");

            migrationBuilder.DropTable(
                name: "IngredientStocks");

            migrationBuilder.DropTable(
                name: "ItemMeal");

            migrationBuilder.DropTable(
                name: "PromoCodes");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropColumn(
                name: "PriceDaily",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "DaysLeft",
                table: "Subscriptions",
                newName: "PaymentStatus");

            migrationBuilder.RenameColumn(
                name: "PriceWeekly",
                table: "Plans",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "PriceMonthly",
                table: "Plans",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "WeightRaw",
                table: "Items",
                newName: "ModifiedAtAt");

            migrationBuilder.RenameColumn(
                name: "IsMainItem",
                table: "Items",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Carbohydrates",
                table: "Items",
                newName: "ImageUrls");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Ingredients",
                newName: "StockQuantity");

            migrationBuilder.RenameColumn(
                name: "AmountNeeded",
                table: "Ingredients",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "AppliedReferralCodeId",
                table: "Subscriptions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAtAt",
                table: "Subscriptions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Plans",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Plans",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreDefined",
                table: "Plans",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAtAt",
                table: "Plans",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Calories",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Carbs",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Fibers",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserPrefernceId",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Ingredients",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Ingredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAtAt",
                table: "Ingredients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserPrefernceId",
                table: "Ingredients",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BreakFastOrDinnerMeals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlanId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalCalories = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalFats = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalCarbs = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalProteins = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<uint>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakFastOrDinnerMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreakFastOrDinnerMeals_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BreakFastOrDinnerMeals_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtraItemOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Weight = table.Column<decimal>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
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
                name: "IngredientChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    OldValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    NewValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
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
                name: "ItemIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
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
                name: "LunchMeals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlansId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ProteinItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CarbohydrateItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalCalories = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalFats = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalCarbs = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalProteins = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<uint>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LunchMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LunchMeals_Items_CarbohydrateItemId",
                        column: x => x.CarbohydrateItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LunchMeals_Items_ProteinItemId",
                        column: x => x.ProteinItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LunchMeals_Plans_PlansId",
                        column: x => x.PlansId,
                        principalTable: "Plans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReferralCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferralCodes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_AppliedReferralCodeId",
                table: "Subscriptions",
                column: "AppliedReferralCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_CreatedByUserId",
                table: "Plans",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserPrefernceId",
                table: "Items",
                column: "UserPrefernceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_UserPrefernceId",
                table: "Ingredients",
                column: "UserPrefernceId");

            migrationBuilder.CreateIndex(
                name: "IX_BreakFastOrDinnerMeals_ItemId",
                table: "BreakFastOrDinnerMeals",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BreakFastOrDinnerMeals_PlanId",
                table: "BreakFastOrDinnerMeals",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraItemOptions_ItemId",
                table: "ExtraItemOptions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientChanges_IngredientId",
                table: "IngredientChanges",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemIngredients_IngredientId",
                table: "ItemIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemIngredients_ItemId",
                table: "ItemIngredients",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LunchMeals_CarbohydrateItemId",
                table: "LunchMeals",
                column: "CarbohydrateItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LunchMeals_PlansId",
                table: "LunchMeals",
                column: "PlansId");

            migrationBuilder.CreateIndex(
                name: "IX_LunchMeals_ProteinItemId",
                table: "LunchMeals",
                column: "ProteinItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralCodes_UserId",
                table: "ReferralCodes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_UserPrefernces_UserPrefernceId",
                table: "Ingredients",
                column: "UserPrefernceId",
                principalTable: "UserPrefernces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_UserPrefernces_UserPrefernceId",
                table: "Items",
                column: "UserPrefernceId",
                principalTable: "UserPrefernces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_AspNetUsers_CreatedByUserId",
                table: "Plans",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_ReferralCodes_AppliedReferralCodeId",
                table: "Subscriptions",
                column: "AppliedReferralCodeId",
                principalTable: "ReferralCodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_UserPrefernces_UserPrefernceId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_UserPrefernces_UserPrefernceId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_AspNetUsers_CreatedByUserId",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_ReferralCodes_AppliedReferralCodeId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "BreakFastOrDinnerMeals");

            migrationBuilder.DropTable(
                name: "ExtraItemOptions");

            migrationBuilder.DropTable(
                name: "IngredientChanges");

            migrationBuilder.DropTable(
                name: "ItemIngredients");

            migrationBuilder.DropTable(
                name: "LunchMeals");

            migrationBuilder.DropTable(
                name: "ReferralCodes");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_AppliedReferralCodeId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Plans_CreatedByUserId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Items_UserPrefernceId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_UserPrefernceId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "AppliedReferralCodeId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ModifiedAtAt",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "IsPreDefined",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "ModifiedAtAt",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Carbs",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Fibers",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UserPrefernceId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ModifiedAtAt",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "UserPrefernceId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Subscriptions",
                newName: "DaysLeft");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Plans",
                newName: "PriceWeekly");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Plans",
                newName: "PriceMonthly");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Items",
                newName: "IsMainItem");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtAt",
                table: "Items",
                newName: "WeightRaw");

            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "Items",
                newName: "Carbohydrates");

            migrationBuilder.RenameColumn(
                name: "StockQuantity",
                table: "Ingredients",
                newName: "Stock");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Ingredients",
                newName: "AmountNeeded");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceDaily",
                table: "Plans",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Items",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "Calories",
                table: "Items",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "IngredientLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Stock = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientStocks_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Calories = table.Column<decimal>(type: "TEXT", nullable: true),
                    Carbohydrates = table.Column<decimal>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Fats = table.Column<decimal>(type: "TEXT", nullable: false),
                    MealType = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Proteins = table.Column<decimal>(type: "TEXT", nullable: false),
                    UserPrefernceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Weight = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_UserPrefernces_UserPrefernceId",
                        column: x => x.UserPrefernceId,
                        principalTable: "UserPrefernces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PromoCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Discount = table.Column<decimal>(type: "TEXT", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

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
                        name: "FK_ItemMeal_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
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
                name: "IX_IngredientStocks_IngredientId",
                table: "IngredientStocks",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMeal_MealsId",
                table: "ItemMeal",
                column: "MealsId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_UserPrefernceId",
                table: "Meals",
                column: "UserPrefernceId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_ItemId",
                table: "RecipeIngredients",
                column: "ItemId");
        }
    }
}
