using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class miga3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_AppUserId",
                table: "Addresses");

            migrationBuilder.DeleteData(
                table: "AboutUs",
                keyColumn: "Id",
                keyValue: new Guid("c0ac9880-d25d-4006-9c3b-95f2cfa7dfa0"));

            migrationBuilder.DeleteData(
                table: "Contact",
                keyColumn: "Id",
                keyValue: new Guid("91bc4d3f-50a0-4b7c-b0ea-340af870eecb"));

            migrationBuilder.DeleteData(
                table: "HelpCenters",
                keyColumn: "Id",
                keyValue: new Guid("e964edaa-2b01-46b9-acaa-74e045e1fe45"));

            migrationBuilder.InsertData(
                table: "AboutUs",
                columns: new[] { "Id", "BusinessInfo", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "WhatWeDo" },
                values: new object[] { new Guid("9be390a3-eb85-4469-8060-6bfdfc35baa6"), "Our mission is to have a positive impact by offering a seamless e-commerce experience to customers and sellers.\r\n\r\n\r\nThe trust of around 30 million customers and 250,000 sellers propelled us to become the first decacorn in Türkiye and one of the top e-commerce platforms in the world.", new DateTime(2024, 9, 16, 2, 0, 42, 781, DateTimeKind.Local).AddTicks(9097), null, false, null, "Our goal is to make fashion accessible for everyone, where trends meet affordability." });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "Address", "CreatedDate", "DeletedDate", "Email", "IsDeleted", "ModifiedDate", "Phone", "Title" },
                values: new object[] { new Guid("f9b93d83-9b03-42b5-b3f8-a74cc1c81c30"), "Ankara/Çankaya Türkiye", new DateTime(2024, 9, 16, 2, 0, 42, 781, DateTimeKind.Local).AddTicks(8849), null, "boostAnk17@ecommerce.com", false, null, "(+90) 530 000 00 00", "Title of ECommerce Site" });

            migrationBuilder.InsertData(
                table: "HelpCenters",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "PopularAnswer", "PopularQuestion" },
                values: new object[] { new Guid("0f110687-8d5e-4cc5-bc79-19ba9c82c47e"), new DateTime(2024, 9, 16, 2, 0, 42, 781, DateTimeKind.Local).AddTicks(5539), null, false, null, "If you’re not happy with your order, you have 30 days to send us back any items.<br>\r\n To make a return, simply create a return label (check ''How do I get a return label?'' for more details) for each delivery you received and pack the items you are returning. Make sure to cover up or remove the original delivery bar code on the box and add the right labels for the respective items and package.<br>\r\nDone! You can drop off your return at a parcel shop. Don’t forget to keep the return receipt from the courier until your refund has been processed.<br>\r\n We will process your return and the total amount will be reimbursed to you through the same payment method that you used for the purchase. Please keep in mind that the time to refund may vary according to your payment method.", "How can I return my order?" });

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_AppUserId",
                table: "Addresses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_AppUserId",
                table: "Addresses");

            migrationBuilder.DeleteData(
                table: "AboutUs",
                keyColumn: "Id",
                keyValue: new Guid("9be390a3-eb85-4469-8060-6bfdfc35baa6"));

            migrationBuilder.DeleteData(
                table: "Contact",
                keyColumn: "Id",
                keyValue: new Guid("f9b93d83-9b03-42b5-b3f8-a74cc1c81c30"));

            migrationBuilder.DeleteData(
                table: "HelpCenters",
                keyColumn: "Id",
                keyValue: new Guid("0f110687-8d5e-4cc5-bc79-19ba9c82c47e"));

            migrationBuilder.InsertData(
                table: "AboutUs",
                columns: new[] { "Id", "BusinessInfo", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "WhatWeDo" },
                values: new object[] { new Guid("c0ac9880-d25d-4006-9c3b-95f2cfa7dfa0"), "Our mission is to have a positive impact by offering a seamless e-commerce experience to customers and sellers.\r\n\r\n\r\nThe trust of around 30 million customers and 250,000 sellers propelled us to become the first decacorn in Türkiye and one of the top e-commerce platforms in the world.", new DateTime(2024, 9, 15, 20, 17, 24, 170, DateTimeKind.Local).AddTicks(5350), null, false, null, "Our goal is to make fashion accessible for everyone, where trends meet affordability." });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "Address", "CreatedDate", "DeletedDate", "Email", "IsDeleted", "ModifiedDate", "Phone", "Title" },
                values: new object[] { new Guid("91bc4d3f-50a0-4b7c-b0ea-340af870eecb"), "Ankara/Çankaya Türkiye", new DateTime(2024, 9, 15, 20, 17, 24, 170, DateTimeKind.Local).AddTicks(5278), null, "boostAnk17@ecommerce.com", false, null, "(+90) 530 000 00 00", "Title of ECommerce Site" });

            migrationBuilder.InsertData(
                table: "HelpCenters",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "PopularAnswer", "PopularQuestion" },
                values: new object[] { new Guid("e964edaa-2b01-46b9-acaa-74e045e1fe45"), new DateTime(2024, 9, 15, 20, 17, 24, 170, DateTimeKind.Local).AddTicks(2143), null, false, null, "If you’re not happy with your order, you have 30 days to send us back any items.<br>\r\n To make a return, simply create a return label (check ''How do I get a return label?'' for more details) for each delivery you received and pack the items you are returning. Make sure to cover up or remove the original delivery bar code on the box and add the right labels for the respective items and package.<br>\r\nDone! You can drop off your return at a parcel shop. Don’t forget to keep the return receipt from the courier until your refund has been processed.<br>\r\n We will process your return and the total amount will be reimbursed to you through the same payment method that you used for the purchase. Please keep in mind that the time to refund may vary according to your payment method.", "How can I return my order?" });

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_AppUserId",
                table: "Addresses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
