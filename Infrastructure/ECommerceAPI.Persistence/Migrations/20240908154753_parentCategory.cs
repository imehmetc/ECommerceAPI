using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class parentCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "AboutUs",
                keyColumn: "Id",
                keyValue: new Guid("def11d1b-15ea-4c39-8970-3316269d81f8"));

            migrationBuilder.DeleteData(
                table: "Contact",
                keyColumn: "Id",
                keyValue: new Guid("567d44be-893a-4879-b032-c4ca55c2b9e4"));

            migrationBuilder.DeleteData(
                table: "HelpCenters",
                keyColumn: "Id",
                keyValue: new Guid("4dfd10eb-e9a7-4d47-93fa-99a753f29fdb"));

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
                name: "IX_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                table: "Categories");

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

            migrationBuilder.InsertData(
                table: "AboutUs",
                columns: new[] { "Id", "BusinessInfo", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "WhatWeDo" },
                values: new object[] { new Guid("def11d1b-15ea-4c39-8970-3316269d81f8"), "Our mission is to have a positive impact by offering a seamless e-commerce experience to customers and sellers.\r\n\r\n\r\nThe trust of around 30 million customers and 250,000 sellers propelled us to become the first decacorn in Türkiye and one of the top e-commerce platforms in the world.", new DateTime(2024, 9, 8, 16, 45, 50, 591, DateTimeKind.Local).AddTicks(5767), null, false, null, "Our goal is to make fashion accessible for everyone, where trends meet affordability." });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "Address", "CreatedDate", "DeletedDate", "Email", "IsDeleted", "ModifiedDate", "Phone", "Title" },
                values: new object[] { new Guid("567d44be-893a-4879-b032-c4ca55c2b9e4"), "Ankara/Çankaya Türkiye", new DateTime(2024, 9, 8, 16, 45, 50, 591, DateTimeKind.Local).AddTicks(5674), null, "boostAnk17@ecommerce.com", false, null, "(+90) 530 000 00 00", "Title of ECommerce Site" });

            migrationBuilder.InsertData(
                table: "HelpCenters",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "PopularAnswer", "PopularQuestion" },
                values: new object[] { new Guid("4dfd10eb-e9a7-4d47-93fa-99a753f29fdb"), new DateTime(2024, 9, 8, 16, 45, 50, 591, DateTimeKind.Local).AddTicks(2905), null, false, null, "If you’re not happy with your order, you have 30 days to send us back any items.<br>\r\n To make a return, simply create a return label (check ''How do I get a return label?'' for more details) for each delivery you received and pack the items you are returning. Make sure to cover up or remove the original delivery bar code on the box and add the right labels for the respective items and package.<br>\r\nDone! You can drop off your return at a parcel shop. Don’t forget to keep the return receipt from the courier until your refund has been processed.<br>\r\n We will process your return and the total amount will be reimbursed to you through the same payment method that you used for the purchase. Please keep in mind that the time to refund may vary according to your payment method.", "How can I return my order?" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
