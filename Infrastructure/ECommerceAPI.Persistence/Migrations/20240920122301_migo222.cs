using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migo222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AboutUs",
                keyColumn: "Id",
                keyValue: new Guid("11e97099-7093-4798-82ac-d9abdbdfff5b"));

            migrationBuilder.DeleteData(
                table: "Contact",
                keyColumn: "Id",
                keyValue: new Guid("1ef47052-a0fa-4b62-9fb0-49e6abb6afdd"));

            migrationBuilder.DeleteData(
                table: "HelpCenters",
                keyColumn: "Id",
                keyValue: new Guid("b5d8a24b-d38e-4349-8a1c-b4570c727b97"));

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Carts");

            migrationBuilder.InsertData(
                table: "AboutUs",
                columns: new[] { "Id", "BusinessInfo", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "WhatWeDo" },
                values: new object[] { new Guid("5c593cfb-8115-48ec-94f5-0f438ec0713a"), "Our mission is to have a positive impact by offering a seamless e-commerce experience to customers and sellers.\r\n\r\n\r\nThe trust of around 30 million customers and 250,000 sellers propelled us to become the first decacorn in Türkiye and one of the top e-commerce platforms in the world.", new DateTime(2024, 9, 20, 15, 23, 0, 696, DateTimeKind.Local).AddTicks(7831), null, false, null, "Our goal is to make fashion accessible for everyone, where trends meet affordability." });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "Address", "CreatedDate", "DeletedDate", "Email", "IsDeleted", "ModifiedDate", "Phone", "Title" },
                values: new object[] { new Guid("80db63b3-dbb5-4514-8826-6f49e7691abe"), "Ankara/Çankaya Türkiye", new DateTime(2024, 9, 20, 15, 23, 0, 696, DateTimeKind.Local).AddTicks(7779), null, "boostAnk17@ecommerce.com", false, null, "(+90) 530 000 00 00", "Title of ECommerce Site" });

            migrationBuilder.InsertData(
                table: "HelpCenters",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "PopularAnswer", "PopularQuestion" },
                values: new object[] { new Guid("dafdf84f-0c46-41ec-856d-1466dea61cdd"), new DateTime(2024, 9, 20, 15, 23, 0, 696, DateTimeKind.Local).AddTicks(5439), null, false, null, "If you’re not happy with your order, you have 30 days to send us back any items.<br>\r\n To make a return, simply create a return label (check ''How do I get a return label?'' for more details) for each delivery you received and pack the items you are returning. Make sure to cover up or remove the original delivery bar code on the box and add the right labels for the respective items and package.<br>\r\nDone! You can drop off your return at a parcel shop. Don’t forget to keep the return receipt from the courier until your refund has been processed.<br>\r\n We will process your return and the total amount will be reimbursed to you through the same payment method that you used for the purchase. Please keep in mind that the time to refund may vary according to your payment method.", "How can I return my order?" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AboutUs",
                keyColumn: "Id",
                keyValue: new Guid("5c593cfb-8115-48ec-94f5-0f438ec0713a"));

            migrationBuilder.DeleteData(
                table: "Contact",
                keyColumn: "Id",
                keyValue: new Guid("80db63b3-dbb5-4514-8826-6f49e7691abe"));

            migrationBuilder.DeleteData(
                table: "HelpCenters",
                keyColumn: "Id",
                keyValue: new Guid("dafdf84f-0c46-41ec-856d-1466dea61cdd"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AboutUs",
                columns: new[] { "Id", "BusinessInfo", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "WhatWeDo" },
                values: new object[] { new Guid("11e97099-7093-4798-82ac-d9abdbdfff5b"), "Our mission is to have a positive impact by offering a seamless e-commerce experience to customers and sellers.\r\n\r\n\r\nThe trust of around 30 million customers and 250,000 sellers propelled us to become the first decacorn in Türkiye and one of the top e-commerce platforms in the world.", new DateTime(2024, 9, 20, 15, 18, 18, 191, DateTimeKind.Local).AddTicks(9194), null, false, null, "Our goal is to make fashion accessible for everyone, where trends meet affordability." });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "Address", "CreatedDate", "DeletedDate", "Email", "IsDeleted", "ModifiedDate", "Phone", "Title" },
                values: new object[] { new Guid("1ef47052-a0fa-4b62-9fb0-49e6abb6afdd"), "Ankara/Çankaya Türkiye", new DateTime(2024, 9, 20, 15, 18, 18, 191, DateTimeKind.Local).AddTicks(9094), null, "boostAnk17@ecommerce.com", false, null, "(+90) 530 000 00 00", "Title of ECommerce Site" });

            migrationBuilder.InsertData(
                table: "HelpCenters",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "PopularAnswer", "PopularQuestion" },
                values: new object[] { new Guid("b5d8a24b-d38e-4349-8a1c-b4570c727b97"), new DateTime(2024, 9, 20, 15, 18, 18, 191, DateTimeKind.Local).AddTicks(3055), null, false, null, "If you’re not happy with your order, you have 30 days to send us back any items.<br>\r\n To make a return, simply create a return label (check ''How do I get a return label?'' for more details) for each delivery you received and pack the items you are returning. Make sure to cover up or remove the original delivery bar code on the box and add the right labels for the respective items and package.<br>\r\nDone! You can drop off your return at a parcel shop. Don’t forget to keep the return receipt from the courier until your refund has been processed.<br>\r\n We will process your return and the total amount will be reimbursed to you through the same payment method that you used for the purchase. Please keep in mind that the time to refund may vary according to your payment method.", "How can I return my order?" });
        }
    }
}
