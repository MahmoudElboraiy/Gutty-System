using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Plans_PlanId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Plans_AspNetUsers_CreatedByUserId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_CreatedByUserId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Meals_PlanId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "IsPreDefined",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Plans",
                newName: "Price");

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

            migrationBuilder.AddColumn<bool>(
                name: "HasCarb",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MenuType",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "HasCarb",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MenuType",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Plans",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Plans",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreDefined",
                table: "Plans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "Meals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Plans_CreatedByUserId",
                table: "Plans",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_PlanId",
                table: "Meals",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Plans_PlanId",
                table: "Meals",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_AspNetUsers_CreatedByUserId",
                table: "Plans",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
