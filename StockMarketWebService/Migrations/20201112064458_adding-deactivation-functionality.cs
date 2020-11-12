using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMarketWebService.Migrations
{
    public partial class addingdeactivationfunctionality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Companies",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Companies");
        }
    }
}
