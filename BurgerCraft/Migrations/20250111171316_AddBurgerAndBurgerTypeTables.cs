using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BurgerCraft.Migrations
{
    /// <inheritdoc />
    public partial class AddBurgerAndBurgerTypeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BurgerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BurgerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Burgers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    BurgerTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Burgers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Burgers_BurgerTypes_BurgerTypeId",
                        column: x => x.BurgerTypeId,
                        principalTable: "BurgerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BurgerTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Veggie" },
                    { 2, "Chicken" },
                    { 3, "Beef" }
                });

            migrationBuilder.InsertData(
                table: "Burgers",
                columns: new[] { "Id", "BurgerTypeId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Fresh veggie patty with lettuce and tomato", "Veggie Delight", 5.99m },
                    { 2, 2, "Grilled chicken with mayo and lettuce", "Chicken Supreme", 6.99m },
                    { 3, 3, "Juicy beef patty with cheddar cheese", "Classic Beef", 7.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Burgers_BurgerTypeId",
                table: "Burgers",
                column: "BurgerTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Burgers");

            migrationBuilder.DropTable(
                name: "BurgerTypes");
        }
    }
}
