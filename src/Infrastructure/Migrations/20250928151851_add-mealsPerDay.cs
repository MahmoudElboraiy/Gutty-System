using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addmealsPerDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfLunchMeals",
                table: "Plans",
                newName: "LMealsPerDay");

            migrationBuilder.AddColumn<long>(
                name: "BDMealsPerDay",
                table: "Plans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BDMealsPerDay",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "LMealsPerDay",
                table: "Plans",
                newName: "NumberOfLunchMeals");
        }
    }
}
