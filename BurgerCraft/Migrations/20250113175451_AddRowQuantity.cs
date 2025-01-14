using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerCraft.Migrations
{
    /// <inheritdoc />
    public partial class AddRowQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "burgerIngredients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "burgerIngredients",
                keyColumns: new[] { "BurgerId", "IngredientId" },
                keyValues: new object[] { 1, 1 },
                column: "Quantity",
                value: 1);

            migrationBuilder.UpdateData(
                table: "burgerIngredients",
                keyColumns: new[] { "BurgerId", "IngredientId" },
                keyValues: new object[] { 1, 2 },
                column: "Quantity",
                value: 1);

            migrationBuilder.UpdateData(
                table: "burgerIngredients",
                keyColumns: new[] { "BurgerId", "IngredientId" },
                keyValues: new object[] { 2, 1 },
                column: "Quantity",
                value: 1);

            migrationBuilder.UpdateData(
                table: "burgerIngredients",
                keyColumns: new[] { "BurgerId", "IngredientId" },
                keyValues: new object[] { 2, 3 },
                column: "Quantity",
                value: 1);

            migrationBuilder.UpdateData(
                table: "burgerIngredients",
                keyColumns: new[] { "BurgerId", "IngredientId" },
                keyValues: new object[] { 3, 2 },
                column: "Quantity",
                value: 1);

            migrationBuilder.UpdateData(
                table: "burgerIngredients",
                keyColumns: new[] { "BurgerId", "IngredientId" },
                keyValues: new object[] { 3, 4 },
                column: "Quantity",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "burgerIngredients");
        }
    }
}
