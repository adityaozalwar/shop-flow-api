using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopFlowAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntityRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "cart_header",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "cart_header_id",
                table: "cart_details",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_cart_header_id",
                table: "order",
                column: "cart_header_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inventory_product_id",
                table: "inventory",
                column: "product_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cart_header_user_id",
                table: "cart_header",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cart_details_cart_header_id",
                table: "cart_details",
                column: "cart_header_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_details_cart_header_cart_header_id",
                table: "cart_details",
                column: "cart_header_id",
                principalTable: "cart_header",
                principalColumn: "cart_header_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_header_AspNetUsers_user_id",
                table: "cart_header",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_product_product_id",
                table: "inventory",
                column: "product_id",
                principalTable: "product",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_cart_header_cart_header_id",
                table: "order",
                column: "cart_header_id",
                principalTable: "cart_header",
                principalColumn: "cart_header_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_category_id",
                table: "product",
                column: "category_id",
                principalTable: "category",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_details_cart_header_cart_header_id",
                table: "cart_details");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_header_AspNetUsers_user_id",
                table: "cart_header");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_product_product_id",
                table: "inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_order_cart_header_cart_header_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_product_category_category_id",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_category_id",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_order_cart_header_id",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_inventory_product_id",
                table: "inventory");

            migrationBuilder.DropIndex(
                name: "IX_cart_header_user_id",
                table: "cart_header");

            migrationBuilder.DropIndex(
                name: "IX_cart_details_cart_header_id",
                table: "cart_details");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "cart_header");

            migrationBuilder.DropColumn(
                name: "cart_header_id",
                table: "cart_details");
        }
    }
}
