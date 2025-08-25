using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removemaxmeals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxMeals",
                table: "PlanCategories");

            migrationBuilder.AddColumn<long>(
                name: "NumberOfLunchMeals",
                table: "Subscriptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfLunchMeals",
                table: "Subscriptions");

            migrationBuilder.AddColumn<long>(
                name: "MaxMeals",
                table: "PlanCategories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
