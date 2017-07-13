using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SOA.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateConfirmed = table.Column<DateTime>(nullable: false),
                    DateNeeded = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    InternalPn = table.Column<string>(nullable: true),
                    Mfg = table.Column<string>(nullable: true),
                    MfgPn = table.Column<string>(nullable: true),
                    PlaceOrderDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: true),
                    PurchaseTerms = table.Column<string>(nullable: true),
                    Qty = table.Column<string>(nullable: true),
                    QuoteDate = table.Column<DateTime>(nullable: false),
                    RFQDate = table.Column<DateTime>(nullable: false),
                    SaleTerms = table.Column<string>(nullable: true),
                    ShipCourier = table.Column<string>(nullable: true),
                    ShipDate = table.Column<DateTime>(nullable: true),
                    ShipMethod = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TargetPrice = table.Column<double>(nullable: true),
                    TrackNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
