using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitPlanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakfastMealsPerDay",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "DinnerMealsPerDay",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "LunchMealsPerDay",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "MaxChicken",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "MaxHighCarb",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "MaxMeat",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "MaxPizza",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "MaxSeaFood",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "MaxTwagen",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Plans",
                newName: "DinnerPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "BreakfastPrice",
                table: "Plans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PlanCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NumberOfMeals = table.Column<long>(type: "bigint", nullable: false),
                    ProteinGrams = table.Column<long>(type: "bigint", nullable: false),
                    PricePerGram = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AllowProteinChange = table.Column<bool>(type: "bit", nullable: false),
                    MaxMeals = table.Column<long>(type: "bigint", nullable: false),
                    MaxProteinGrams = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanCategories_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanCategories_PlanId",
                table: "PlanCategories",
                column: "PlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanCategories");

            migrationBuilder.DropColumn(
                name: "BreakfastPrice",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "DinnerPrice",
                table: "Plans",
                newName: "Price");

            migrationBuilder.AddColumn<long>(
                name: "BreakfastMealsPerDay",
                table: "Plans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DinnerMealsPerDay",
                table: "Plans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LunchMealsPerDay",
                table: "Plans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MaxChicken",
                table: "Plans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MaxHighCarb",
                table: "Plans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MaxMeat",
                table: "Plans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MaxPizza",
                table: "Plans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MaxSeaFood",
                table: "Plans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MaxTwagen",
                table: "Plans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
