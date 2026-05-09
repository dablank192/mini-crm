using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mini_crm.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecifyDataTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TotalSpent = table.Column<decimal>(type: "TEXT", nullable: false),
                    LatestPurchaseDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientDetail_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartnerDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    TaxIdentifierNumber = table.Column<string>(type: "TEXT", nullable: true),
                    ContractNumber = table.Column<string>(type: "TEXT", nullable: false),
                    ContractEffectiveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ContractExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalRevenue = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerDetail_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    TaxIdentifierNumber = table.Column<string>(type: "TEXT", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "TEXT", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "TEXT", nullable: true),
                    BankName = table.Column<string>(type: "TEXT", nullable: true),
                    CurrentDebt = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorDetail_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientDetail_CustomerId",
                table: "ClientDetail",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartnerDetail_CustomerId",
                table: "PartnerDetail",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorDetail_CustomerId",
                table: "VendorDetail",
                column: "CustomerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientDetail");

            migrationBuilder.DropTable(
                name: "PartnerDetail");

            migrationBuilder.DropTable(
                name: "VendorDetail");
        }
    }
}
