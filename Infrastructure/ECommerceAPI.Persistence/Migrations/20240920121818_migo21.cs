using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migo21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AboutUs",
                keyColumn: "Id",
                keyValue: new Guid("16025e26-ed4e-407d-bc7f-eef6f0dee97d"));

            migrationBuilder.DeleteData(
                table: "Contact",
                keyColumn: "Id",
                keyValue: new Guid("994ed576-88cb-4a92-9cda-4cecb73d77cb"));

            migrationBuilder.DeleteData(
                table: "HelpCenters",
                keyColumn: "Id",
                keyValue: new Guid("2f9fae75-0a20-4d70-a333-a3ab1c2c1a7f"));

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AboutUs",
                columns: new[] { "Id", "BusinessInfo", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "WhatWeDo" },
                values: new object[] { new Guid("16025e26-ed4e-407d-bc7f-eef6f0dee97d"), "Our mission is to have a positive impact by offering a seamless e-commerce experience to customers and sellers.\r\n\r\n\r\nThe trust of around 30 million customers and 250,000 sellers propelled us to become the first decacorn in Türkiye and one of the top e-commerce platforms in the world.", new DateTime(2024, 9, 20, 15, 14, 48, 83, DateTimeKind.Local).AddTicks(4112), null, false, null, "Our goal is to make fashion accessible for everyone, where trends meet affordability." });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "Address", "CreatedDate", "DeletedDate", "Email", "IsDeleted", "ModifiedDate", "Phone", "Title" },
                values: new object[] { new Guid("994ed576-88cb-4a92-9cda-4cecb73d77cb"), "Ankara/Çankaya Türkiye", new DateTime(2024, 9, 20, 15, 14, 48, 83, DateTimeKind.Local).AddTicks(4052), null, "boostAnk17@ecommerce.com", false, null, "(+90) 530 000 00 00", "Title of ECommerce Site" });

            migrationBuilder.InsertData(
                table: "HelpCenters",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "IsDeleted", "ModifiedDate", "PopularAnswer", "PopularQuestion" },
                values: new object[] { new Guid("2f9fae75-0a20-4d70-a333-a3ab1c2c1a7f"), new DateTime(2024, 9, 20, 15, 14, 48, 83, DateTimeKind.Local).AddTicks(449), null, false, null, "If you’re not happy with your order, you have 30 days to send us back any items.<br>\r\n To make a return, simply create a return label (check ''How do I get a return label?'' for more details) for each delivery you received and pack the items you are returning. Make sure to cover up or remove the original delivery bar code on the box and add the right labels for the respective items and package.<br>\r\nDone! You can drop off your return at a parcel shop. Don’t forget to keep the return receipt from the courier until your refund has been processed.<br>\r\n We will process your return and the total amount will be reimbursed to you through the same payment method that you used for the purchase. Please keep in mind that the time to refund may vary according to your payment method.", "How can I return my order?" });
        }
    }
}
