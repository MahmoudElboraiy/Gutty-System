using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Plans_PlanId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_ReferralCodes_AppliedReferralCodeId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_PlanId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Subscriptions",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "AppliedReferralCodeId",
                table: "Subscriptions",
                newName: "ReferralCodeId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Subscriptions",
                newName: "DinnerPrice");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_AppliedReferralCodeId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_ReferralCodeId");

            migrationBuilder.AddColumn<decimal>(
                name: "BreakfastPrice",
                table: "Subscriptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "DurationInDays",
                table: "Subscriptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MaxPastaCarbGrams",
                table: "Subscriptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MaxRiceCarbGrams",
                table: "Subscriptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PastaCarbGrams",
                table: "Subscriptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "PlanName",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PromoCodeId",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RiceCarbGrams",
                table: "Subscriptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionId",
                table: "PlanCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PromoCodeId",
                table: "Subscriptions",
                column: "PromoCodeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_PromoCodes_PromoCodeId",
                table: "Subscriptions",
                column: "PromoCodeId",
                principalTable: "PromoCodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_ReferralCodes_ReferralCodeId",
                table: "Subscriptions",
                column: "ReferralCodeId",
                principalTable: "ReferralCodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanCategories_Subscriptions_SubscriptionId",
                table: "PlanCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_PromoCodes_PromoCodeId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_ReferralCodes_ReferralCodeId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_PromoCodeId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_PlanCategories_SubscriptionId",
                table: "PlanCategories");

            migrationBuilder.DropColumn(
                name: "BreakfastPrice",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "DurationInDays",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "MaxPastaCarbGrams",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "MaxRiceCarbGrams",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PastaCarbGrams",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PlanName",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PromoCodeId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "RiceCarbGrams",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "PlanCategories");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Subscriptions",
                newName: "PaymentDate");

            migrationBuilder.RenameColumn(
                name: "ReferralCodeId",
                table: "Subscriptions",
                newName: "AppliedReferralCodeId");

            migrationBuilder.RenameColumn(
                name: "DinnerPrice",
                table: "Subscriptions",
                newName: "Amount");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_ReferralCodeId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_AppliedReferralCodeId");

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_ReferralCodes_AppliedReferralCodeId",
                table: "Subscriptions",
                column: "AppliedReferralCodeId",
                principalTable: "ReferralCodes",
                principalColumn: "Id");
        }
    }
}
