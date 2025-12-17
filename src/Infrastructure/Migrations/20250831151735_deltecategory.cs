using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deltecategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PlanCategories");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PlanCategories");

            migrationBuilder.DropColumn(
                name: "ModifiedAtAt",
                table: "PlanCategories");

            migrationBuilder.DropPrimaryKey(
               name: "PK_PlanCategories",
               table: "PlanCategories");

            migrationBuilder.DropColumn(
               name: "Id",
               table: "PlanCategories");

            // 2- اضف العمود الجديد int Identity
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PlanCategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // 3) نحدد العمود كـ Primary Key
            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanCategories",
                table: "PlanCategories",
                column: "Id");
            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "PlanCategories",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(Guid),
            //    oldType: "uniqueidentifier")
            //    .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<Guid>(
            //    name: "Id",
            //    table: "PlanCategories",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddColumn<int>(
              name: "Id",
              table: "PlanCategories",
              type: "int",
              nullable: false,
              defaultValue: 0)
              .Annotation("SqlServer:Identity", "1, 1");

            // 3) نحدد العمود كـ Primary Key
            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanCategories",
                table: "PlanCategories",
                column: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PlanCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PlanCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAtAt",
                table: "PlanCategories",
                type: "datetime2",
                nullable: true);
        }
    }
}
