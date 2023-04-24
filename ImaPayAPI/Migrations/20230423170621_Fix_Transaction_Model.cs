using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImaPayAPI.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Transaction_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Transactions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
