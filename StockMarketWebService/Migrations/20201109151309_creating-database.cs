using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMarketWebService.Migrations
{
    public partial class creatingdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    SectorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorName = table.Column<string>(maxLength: 30, nullable: false),
                    WriteUp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.SectorID);
                });

            migrationBuilder.CreateTable(
                name: "StockExchanges",
                columns: table => new
                {
                    StockExchangeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExchangeName = table.Column<string>(maxLength: 30, nullable: false),
                    shortName = table.Column<string>(maxLength: 5, nullable: false),
                    BriefWriteUp = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockExchanges", x => x.StockExchangeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    UserType = table.Column<int>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    Mobile = table.Column<long>(nullable: false),
                    confirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(maxLength: 30, nullable: false),
                    TurnOver = table.Column<float>(nullable: false),
                    CEO = table.Column<string>(nullable: false),
                    BoardOfDirectors = table.Column<string>(nullable: true),
                    SectorID = table.Column<int>(nullable: false),
                    WriteUp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyID);
                    table.ForeignKey(
                        name: "FK_Companies_Sectors_SectorID",
                        column: x => x.SectorID,
                        principalTable: "Sectors",
                        principalColumn: "SectorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "companyStockExchanges",
                columns: table => new
                {
                    CompanyID = table.Column<int>(nullable: false),
                    StockExchangeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companyStockExchanges", x => new { x.CompanyID, x.StockExchangeID });
                    table.ForeignKey(
                        name: "FK_companyStockExchanges_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_companyStockExchanges_StockExchanges_StockExchangeID",
                        column: x => x.StockExchangeID,
                        principalTable: "StockExchanges",
                        principalColumn: "StockExchangeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IPODetails",
                columns: table => new
                {
                    ipoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(nullable: false),
                    stockExchanges = table.Column<string>(nullable: false),
                    PricePerShare = table.Column<float>(nullable: false),
                    TotalAvailableShares = table.Column<int>(nullable: false),
                    OpeningDate = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPODetails", x => x.ipoID);
                    table.ForeignKey(
                        name: "FK_IPODetails_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockCodes",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockCodeInExchange = table.Column<int>(nullable: false),
                    NameOfExchange = table.Column<string>(nullable: true),
                    CompanyID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCodes", x => x.SID);
                    table.ForeignKey(
                        name: "FK_StockCodes_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockPrices",
                columns: table => new
                {
                    SerialNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCode = table.Column<int>(nullable: false),
                    CompanyID = table.Column<int>(nullable: true),
                    Exchange = table.Column<string>(nullable: false),
                    price = table.Column<float>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPrices", x => x.SerialNumber);
                    table.ForeignKey(
                        name: "FK_StockPrices_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SectorID",
                table: "Companies",
                column: "SectorID");

            migrationBuilder.CreateIndex(
                name: "IX_companyStockExchanges_StockExchangeID",
                table: "companyStockExchanges",
                column: "StockExchangeID");

            migrationBuilder.CreateIndex(
                name: "IX_IPODetails_CompanyID",
                table: "IPODetails",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_StockCodes_CompanyID",
                table: "StockCodes",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_StockPrices_CompanyID",
                table: "StockPrices",
                column: "CompanyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "companyStockExchanges");

            migrationBuilder.DropTable(
                name: "IPODetails");

            migrationBuilder.DropTable(
                name: "StockCodes");

            migrationBuilder.DropTable(
                name: "StockPrices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "StockExchanges");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Sectors");
        }
    }
}
