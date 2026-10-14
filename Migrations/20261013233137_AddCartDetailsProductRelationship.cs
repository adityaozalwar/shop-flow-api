using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCartDetailsProductRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_category_category_id",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_cart_details_cart_header_id",
                table: "cart_details");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "order",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_cart_details_cart_header_id_product_id",
                table: "cart_details",
                columns: new[] { "cart_header_id", "product_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cart_details_product_id",
                table: "cart_details",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_details_product_product_id",
                table: "cart_details",
                column: "product_id",
                principalTable: "product",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_category_id",
                table: "product",
                column: "category_id",
                principalTable: "category",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_details_product_product_id",
                table: "cart_details");

            migrationBuilder.DropForeignKey(
                name: "FK_product_category_category_id",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_cart_details_cart_header_id_product_id",
                table: "cart_details");

            migrationBuilder.DropIndex(
                name: "IX_cart_details_product_id",
                table: "cart_details");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "order",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_cart_details_cart_header_id",
                table: "cart_details",
                column: "cart_header_id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_category_id",
                table: "product",
                column: "category_id",
                principalTable: "category",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
