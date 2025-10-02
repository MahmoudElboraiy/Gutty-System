using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToSubCat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxMeals",
                table: "SubscriptionCategories");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Subcategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Subcategories");

            migrationBuilder.AddColumn<long>(
                name: "MaxMeals",
                table: "SubscriptionCategories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
