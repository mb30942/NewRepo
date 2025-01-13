using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BurgerCraft.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBurgerIngredientSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "burgerIngredients",
                columns: new[] { "BurgerId", "IngredientId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 3 },
                    { 3, 2 },
                    { 3, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "burgerIngredients",
                keyColumns: new[] { "BurgerId", "IngredientId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "burgerIngredients",
                keyColumns: new[] { "BurgerId", "IngredientId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "burgerIngredients",
                keyColumns: new[] { "BurgerId", "IngredientId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "burgerIngredients",
                keyColumns: new[] { "BurgerId", "IngredientId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "burgerIngredients",
                keyColumns: new[] { "BurgerId", "IngredientId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "burgerIngredients",
                keyColumns: new[] { "BurgerId", "IngredientId" },
                keyValues: new object[] { 3, 4 });
        }
    }
}
