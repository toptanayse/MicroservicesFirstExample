using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stock.API.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Count", "ProductId" },
                values: new object[,]
                {
                    { new Guid("6d599532-8f90-463f-8c9c-13c629c8d6cd"), 2000, new Guid("6b6385cd-19cc-4d55-ae4f-2c88afae44d9") },
                    { new Guid("86dbb111-6e34-4ef3-ac42-fba753041f51"), 1000, new Guid("12c66184-6ca2-445a-ab51-4394f654881e") },
                    { new Guid("a6afa32f-f469-4063-af2a-0b21ae4cfe18"), 3000, new Guid("e237c625-ae53-4fe5-9fdc-5bc681a51d18") },
                    { new Guid("a6eae1ff-f371-421b-9110-80ae265ba4e6"), 500, new Guid("20140d95-12d8-4b4e-8c1a-b300a63b2e33") },
                    { new Guid("f075dfdf-9e8f-41fd-81df-b3e758000083"), 5000, new Guid("89de26b2-665c-4d96-b67d-c974958f2dd3") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
