using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migo17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_MemberId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "AboutUs",
                keyColumn: "Id",
                keyValue: new Guid("f3de321d-59b9-4f7e-ae74-e171b9447b18"));

            migrationBuilder.DeleteData(
                table: "Contact",
                keyColumn: "Id",
                keyValue: new Guid("44e1bbbe-9a9b-4da0-9a6a-ff4834284401"));

            migrationBuilder.DeleteData(
                table: "HelpCenters",
                keyColumn: "Id",
                keyValue: new Guid("e5f39754-5029-4663-9b2b-7675acfcb467"));

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Orders",
                newName: "ProductDtoId");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Orders",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_MemberId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressDtoId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CartId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedDeliveryDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ShippingCost",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsInStock = table.Column<bool>(type: "bit", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOrdered = table.Column<bool>(type: "bit", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AboutUs",
                columns: new[] { "Id", "BusinessInfo", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "WhatWeDo" },
                values: new object[] { new Guid("50a8e785-006a-4d47-a1e8-78aa5815a2c4"), "Our mission is to have a positive impact by offering a seamless e-commerce experience to customers and sellers.\r\n\r\n\r\nThe trust of around 30 million customers and 250,000 sellers propelled us to become the first decacorn in Türkiye and one of the top e-commerce platforms in the world.", new DateTime(2024, 9, 11, 21, 4, 58, 187, DateTimeKind.Local).AddTicks(8205), null, false, null, "Our goal is to make fashion accessible for everyone, where trends meet affordability." });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "Address", "CreatedDate", "DeletedDate", "Email", "IsDeleted", "ModifiedDate", "Phone", "Title" },
                values: new object[] { new Guid("f0302a90-b1c3-4a50-938a-aca292aef0c7"), "Ankara/Çankaya Türkiye", new DateTime(2024, 9, 11, 21, 4, 58, 187, DateTimeKind.Local).AddTicks(8135), null, "boostAnk17@ecommerce.com", false, null, "(+90) 530 000 00 00", "Title of ECommerce Site" });

            migrationBuilder.InsertData(
                table: "HelpCenters",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "PopularAnswer", "PopularQuestion" },
                values: new object[] { new Guid("0ccaec41-6e75-486c-bbab-d545d62e27f1"), new DateTime(2024, 9, 11, 21, 4, 58, 187, DateTimeKind.Local).AddTicks(4576), null, false, null, "If you’re not happy with your order, you have 30 days to send us back any items.<br>\r\n To make a return, simply create a return label (check ''How do I get a return label?'' for more details) for each delivery you received and pack the items you are returning. Make sure to cover up or remove the original delivery bar code on the box and add the right labels for the respective items and package.<br>\r\nDone! You can drop off your return at a parcel shop. Don’t forget to keep the return receipt from the courier until your refund has been processed.<br>\r\n We will process your return and the total amount will be reimbursed to you through the same payment method that you used for the purchase. Please keep in mind that the time to refund may vary according to your payment method.", "How can I return my order?" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CartId",
                table: "Orders",
                column: "CartId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CustomerId",
                table: "CartItems",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Carts_CartId",
                table: "Orders",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Carts_CartId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CartId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "AboutUs",
                keyColumn: "Id",
                keyValue: new Guid("50a8e785-006a-4d47-a1e8-78aa5815a2c4"));

            migrationBuilder.DeleteData(
                table: "Contact",
                keyColumn: "Id",
                keyValue: new Guid("f0302a90-b1c3-4a50-938a-aca292aef0c7"));

            migrationBuilder.DeleteData(
                table: "HelpCenters",
                keyColumn: "Id",
                keyValue: new Guid("0ccaec41-6e75-486c-bbab-d545d62e27f1"));

            migrationBuilder.DropColumn(
                name: "AddressDtoId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EstimatedDeliveryDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingCost",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ProductDtoId",
                table: "Orders",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                newName: "IX_Orders_MemberId");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AboutUs",
                columns: new[] { "Id", "BusinessInfo", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "WhatWeDo" },
                values: new object[] { new Guid("f3de321d-59b9-4f7e-ae74-e171b9447b18"), "Our mission is to have a positive impact by offering a seamless e-commerce experience to customers and sellers.\r\n\r\n\r\nThe trust of around 30 million customers and 250,000 sellers propelled us to become the first decacorn in Türkiye and one of the top e-commerce platforms in the world.", new DateTime(2024, 9, 8, 18, 47, 52, 91, DateTimeKind.Local).AddTicks(4913), null, false, null, "Our goal is to make fashion accessible for everyone, where trends meet affordability." });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "Address", "CreatedDate", "DeletedDate", "Email", "IsDeleted", "ModifiedDate", "Phone", "Title" },
                values: new object[] { new Guid("44e1bbbe-9a9b-4da0-9a6a-ff4834284401"), "Ankara/Çankaya Türkiye", new DateTime(2024, 9, 8, 18, 47, 52, 91, DateTimeKind.Local).AddTicks(4871), null, "boostAnk17@ecommerce.com", false, null, "(+90) 530 000 00 00", "Title of ECommerce Site" });

            migrationBuilder.InsertData(
                table: "HelpCenters",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "PopularAnswer", "PopularQuestion" },
                values: new object[] { new Guid("e5f39754-5029-4663-9b2b-7675acfcb467"), new DateTime(2024, 9, 8, 18, 47, 52, 91, DateTimeKind.Local).AddTicks(4564), null, false, null, "If you’re not happy with your order, you have 30 days to send us back any items.<br>\r\n To make a return, simply create a return label (check ''How do I get a return label?'' for more details) for each delivery you received and pack the items you are returning. Make sure to cover up or remove the original delivery bar code on the box and add the right labels for the respective items and package.<br>\r\nDone! You can drop off your return at a parcel shop. Don’t forget to keep the return receipt from the courier until your refund has been processed.<br>\r\n We will process your return and the total amount will be reimbursed to you through the same payment method that you used for the purchase. Please keep in mind that the time to refund may vary according to your payment method.", "How can I return my order?" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_MemberId",
                table: "Orders",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
