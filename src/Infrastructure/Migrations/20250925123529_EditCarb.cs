using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditCarb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PastaCarbGrams",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "MaxPastaCarbGrams",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "MaxRiceCarbGrams",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "RiceCarbGrams",
                table: "Subscriptions",
                newName: "CarbGrams");

            migrationBuilder.RenameColumn(
                name: "RiceCarbGrams",
                table: "Plans",
                newName: "MaxCarbGrams");

            migrationBuilder.RenameColumn(
                name: "PastaCarbGrams",
                table: "Plans",
                newName: "CarbGrams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarbGrams",
                table: "Subscriptions",
                newName: "RiceCarbGrams");

            migrationBuilder.RenameColumn(
                name: "MaxCarbGrams",
                table: "Plans",
                newName: "RiceCarbGrams");

            migrationBuilder.RenameColumn(
                name: "CarbGrams",
                table: "Plans",
                newName: "PastaCarbGrams");

            migrationBuilder.AddColumn<long>(
                name: "PastaCarbGrams",
                table: "Subscriptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MaxPastaCarbGrams",
                table: "Plans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MaxRiceCarbGrams",
                table: "Plans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
