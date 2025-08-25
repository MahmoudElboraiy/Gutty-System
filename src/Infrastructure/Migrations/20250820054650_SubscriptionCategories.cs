using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanCategories_Subscriptions_SubscriptionId",
                table: "PlanCategories");

            migrationBuilder.DropIndex(
                name: "IX_PlanCategories_SubscriptionId",
                table: "PlanCategories");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "PlanCategories");

            migrationBuilder.CreateTable(
                name: "SubscriptionCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_SubscriptionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionCategories_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionCategories_SubscriptionId",
                table: "SubscriptionCategories",
                column: "SubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionId",
                table: "PlanCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanCategories_SubscriptionId",
                table: "PlanCategories",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanCategories_Subscriptions_SubscriptionId",
                table: "PlanCategories",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");
        }
    }
}
