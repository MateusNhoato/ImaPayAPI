using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImaPayAPI.Migrations
{
    /// <inheritdoc />
    public partial class adding_investments_and_savings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Investments",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Savings",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Investments",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Savings",
                table: "Users");
        }
    }
}
