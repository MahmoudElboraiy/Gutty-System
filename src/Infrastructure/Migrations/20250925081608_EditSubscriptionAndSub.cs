using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditSubscriptionAndSub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakfastPrice",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "DinnerPrice",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PlanName",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "NumberOfLunchMeals",
                table: "Subscriptions",
                newName: "LunchMealsLeft");

            migrationBuilder.RenameColumn(
                name: "DurationInDays",
                table: "Subscriptions",
                newName: "DaysLeft");

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaused",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<long>(
                name: "NumberOfMealsLeft",
                table: "SubscriptionCategories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PlanId",
                table: "Subscriptions",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Plans_PlanId",
                table: "Subscriptions",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Plans_PlanId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_PlanId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "IsPaused",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "NumberOfMealsLeft",
                table: "SubscriptionCategories");

            migrationBuilder.RenameColumn(
                name: "LunchMealsLeft",
                table: "Subscriptions",
                newName: "NumberOfLunchMeals");

            migrationBuilder.RenameColumn(
                name: "DaysLeft",
                table: "Subscriptions",
                newName: "DurationInDays");

            migrationBuilder.AddColumn<decimal>(
                name: "BreakfastPrice",
                table: "Subscriptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DinnerPrice",
                table: "Subscriptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PlanName",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
