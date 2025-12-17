using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixRelationBetweenSubCategoryAndSubscriptoinCat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionCategories_SubCategoryId",
                table: "SubscriptionCategories",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionCategories_Subcategories_SubCategoryId",
                table: "SubscriptionCategories",
                column: "SubCategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionCategories_Subcategories_SubCategoryId",
                table: "SubscriptionCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionCategories_SubCategoryId",
                table: "SubscriptionCategories");
        }
    }
}
