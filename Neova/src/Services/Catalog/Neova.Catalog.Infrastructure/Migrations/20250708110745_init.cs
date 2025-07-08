using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Neova.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 8, 11, 7, 44, 679, DateTimeKind.Utc).AddTicks(8238), "Elektronik ürünler", null, "Elektronik" },
                    { 2, new DateTime(2025, 7, 8, 11, 7, 44, 679, DateTimeKind.Utc).AddTicks(8491), "Giyim ürünleri", null, "Giyim" },
                    { 3, new DateTime(2025, 7, 8, 11, 7, 44, 679, DateTimeKind.Utc).AddTicks(8497), "Ev ve yaşam ürünleri", null, "Ev & Yaşam" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "ImageUrl", "LastModifiedDate", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("1521c8c5-9859-4d55-a782-08949f92325a"),1, new DateTime(2025, 7, 8, 11, 7, 44, 679, DateTimeKind.Utc).AddTicks(8950), "Yüksek performanslı akıllı telefon", "phone.png", null, "Akıllı Telefon", 999.99m, 50 },
                    { new Guid("547391f3-3e83-4693-a59f-6f7b46762e1a"), 2, new DateTime(2025, 7, 8, 11, 7, 44, 679, DateTimeKind.Utc).AddTicks(9011), "Konforlu koltuk takımı", "sofa.png", null, "Koltuk Takımı", 2999.99m, 20 },
                    { new Guid("cde9be93-9c20-4656-a4e0-7188bdf6685f"), 3, new DateTime(2025, 7, 8, 11, 7, 44, 679, DateTimeKind.Utc).AddTicks(9005), "Pamuklu tişört", "tshirt.png", null, "Tişört", 19.99m, 100,  }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
