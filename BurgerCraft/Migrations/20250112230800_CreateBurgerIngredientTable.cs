using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerCraft.Migrations
{
    /// <inheritdoc />
    public partial class CreateBurgerIngredientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "burgerIngredients",
                columns: table => new
                {
                    BurgerId = table.Column<int>(type: "integer", nullable: false),
                    IngredientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_burgerIngredients", x => new { x.BurgerId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_burgerIngredients_Burgers_BurgerId",
                        column: x => x.BurgerId,
                        principalTable: "Burgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_burgerIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_burgerIngredients_IngredientId",
                table: "burgerIngredients",
                column: "IngredientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "burgerIngredients");
        }
    }
}
