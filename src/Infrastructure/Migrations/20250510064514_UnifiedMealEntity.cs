using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UnifiedMealEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "BreakFastOrDinnerMeals");

            migrationBuilder.DropTable(name: "LunchMeals");

            migrationBuilder.DropColumn(name: "EndDate", table: "Subscriptions");

            migrationBuilder.DropColumn(name: "StartDate", table: "Subscriptions");

            migrationBuilder.AddColumn<bool>(
                name: "IsOptional",
                table: "ItemIngredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: false
            );

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlanId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: true),
                    MealType = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalCalories = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalFats = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalCarbs = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalProteins = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<uint>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Meals_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(name: "IX_Meals_ItemId", table: "Meals", column: "ItemId");

            migrationBuilder.CreateIndex(name: "IX_Meals_PlanId", table: "Meals", column: "PlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Meals");

            migrationBuilder.DropColumn(name: "IsOptional", table: "ItemIngredients");

            migrationBuilder.DropColumn(name: "CityId", table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );

            migrationBuilder.CreateTable(
                name: "BreakFastOrDinnerMeals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlanId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Quantity = table.Column<uint>(type: "INTEGER", nullable: false),
                    TotalCalories = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalCarbs = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalFats = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalProteins = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakFastOrDinnerMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreakFastOrDinnerMeals_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_BreakFastOrDinnerMeals_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "LunchMeals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CarbohydrateItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlansId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ProteinItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Quantity = table.Column<uint>(type: "INTEGER", nullable: false),
                    TotalCalories = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalCarbs = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalFats = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalProteins = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LunchMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LunchMeals_Items_CarbohydrateItemId",
                        column: x => x.CarbohydrateItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_LunchMeals_Items_ProteinItemId",
                        column: x => x.ProteinItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_LunchMeals_Plans_PlansId",
                        column: x => x.PlansId,
                        principalTable: "Plans",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_BreakFastOrDinnerMeals_ItemId",
                table: "BreakFastOrDinnerMeals",
                column: "ItemId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_BreakFastOrDinnerMeals_PlanId",
                table: "BreakFastOrDinnerMeals",
                column: "PlanId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_LunchMeals_CarbohydrateItemId",
                table: "LunchMeals",
                column: "CarbohydrateItemId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_LunchMeals_PlansId",
                table: "LunchMeals",
                column: "PlansId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_LunchMeals_ProteinItemId",
                table: "LunchMeals",
                column: "ProteinItemId"
            );
        }
    }
}
