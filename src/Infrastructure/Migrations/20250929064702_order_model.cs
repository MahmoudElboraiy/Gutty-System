using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class order_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderMeals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MealType = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: true),
                    ProteinMealId = table.Column<int>(type: "int", nullable: true),
                    CarbMealId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderMeals_Meals_CarbMealId",
                        column: x => x.CarbMealId,
                        principalTable: "Meals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderMeals_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderMeals_Meals_ProteinMealId",
                        column: x => x.ProteinMealId,
                        principalTable: "Meals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderMeals_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderMeals_CarbMealId",
                table: "OrderMeals",
                column: "CarbMealId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMeals_MealId",
                table: "OrderMeals",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMeals_OrderId",
                table: "OrderMeals",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMeals_ProteinMealId",
                table: "OrderMeals",
                column: "ProteinMealId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SubscriptionId",
                table: "Orders",
                column: "SubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderMeals");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
