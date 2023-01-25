using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Assignment3.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentTerms",
                columns: table => new
                {
                    PaymentTermsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerms", x => x.PaymentTermsId);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceOrState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipOrPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorContactLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorContactFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorId);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentTotal = table.Column<double>(type: "float", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentTermsId = table.Column<int>(type: "int", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_PaymentTerms_PaymentTermsId",
                        column: x => x.PaymentTermsId,
                        principalTable: "PaymentTerms",
                        principalColumn: "PaymentTermsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoicesLineItem",
                columns: table => new
                {
                    InvoiceLineItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicesLineItem", x => x.InvoiceLineItemId);
                    table.ForeignKey(
                        name: "FK_InvoicesLineItem_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId");
                });

            migrationBuilder.InsertData(
                table: "PaymentTerms",
                columns: new[] { "PaymentTermsId", "Description", "DueDays" },
                values: new object[,]
                {
                    { 1, "Net due 10 days", 10 },
                    { 2, "Net due 20 days", 20 },
                    { 3, "Net due 30 days", 30 },
                    { 4, "Net due 60 days", 60 },
                    { 5, "Net due 90 days", 90 }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "Address1", "Address2", "City", "IsDeleted", "Name", "ProvinceOrState", "VendorContactEmail", "VendorContactFirstName", "VendorContactLastName", "VendorPhone", "ZipOrPostalCode" },
                values: new object[,]
                {
                    { 1, "27371 Valderas", null, "Mission Viejo", false, "Blanchard & Johnson Associates", "CA", "kgonz@bja.com", "Keeton", "Gonzalo", "214-555-3647", "92691" },
                    { 2, "3255 Ramos Cir", null, "Sacramento", false, "California Chamber Of Commerce", "CA", "manton@gmail.com", "Mauro", "Anton", "916-555-6670", "95827" },
                    { 3, "PO Box 85826", null, "San Diego", false, "Golden Eagle Insurance Co", "CA", null, "Jane", "Smith", "917-544-7090", "92186" },
                    { 4, "1952 H Street", "P.O. Box 1952", "Fresno", false, "Fresno Photoengraving Company", "CA", null, "Chad", "Jones", "559-555-3005", "93718" },
                    { 5, "Ohio Valley Litho Division", null, "Cincinnate", false, "Nielson", "OH", null, "Paul", "Morgan", "519-824-3477", "45264" },
                    { 6, "PO Box 40513", null, "Jacksonville", false, "Naylor Publications Inc", "FL", "gerald.kristoff@outlook.com", "Gerald", "Kristoff", "800-555-6041", "32231" },
                    { 7, "60 Madison Ave", null, "New York", false, "Venture Communications", "NY", "tneftaly@venture.com", "Thalia", "Neftaly", "212-533-4800", "10010" },
                    { 8, "Attn:  Supt. Window Services", "PO Box 7005", "Madison", false, "US Postal Service", "WI", null, "Alberto", "Francesco", "800-555-1205", "53707" }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "InvoiceDate", "PaymentDate", "PaymentTermsId", "PaymentTotal", "VendorId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 0.0, 1 },
                    { 2, new DateTime(2022, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 0.0, 1 },
                    { 3, new DateTime(2022, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 0.0, 2 },
                    { 4, new DateTime(2022, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, 0.0, 2 },
                    { 5, new DateTime(2022, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 0.0, 3 },
                    { 6, new DateTime(2022, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, 0.0, 3 },
                    { 7, new DateTime(2022, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 0.0, 4 },
                    { 8, new DateTime(2022, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 5, 0.0, 4 },
                    { 9, new DateTime(2022, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 0.0, 5 },
                    { 10, new DateTime(2022, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 0.0, 6 },
                    { 11, new DateTime(2022, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, 0.0, 7 },
                    { 12, new DateTime(2022, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 0.0, 8 }
                });

            migrationBuilder.InsertData(
                table: "InvoicesLineItem",
                columns: new[] { "InvoiceLineItemId", "Amount", "Description", "InvoiceId" },
                values: new object[,]
                {
                    { 1, 1089.99, "Part 123", 1 },
                    { 2, 173499.5, "Service XYZ", 1 },
                    { 3, 4654499.5, "Part 110", 2 },
                    { 4, 78799.5, "Part A", 2 },
                    { 5, 679.5, "Part AA", 3 },
                    { 6, 786.89999999999998, "Part Z", 3 },
                    { 7, 998.5, "Trip 1", 4 },
                    { 8, 1011.5, "Service XYZ", 4 },
                    { 9, 565735.5, "Rental DEF", 5 },
                    { 10, 5753.5, "Rental ZZZ", 5 },
                    { 11, 58453.900000000001, "Service ABC", 6 },
                    { 12, 111232.5, "Service ABC", 6 },
                    { 13, 109.5, "Rental ABC", 7 },
                    { 14, 57846.5, "Rental ABC", 8 },
                    { 15, 132.5, "Trip 2", 9 },
                    { 16, 6877.8999999999996, "Service XYZ", 9 },
                    { 17, 8999.5, "Trip 3", 10 },
                    { 18, 1033.5, "Blah blah", 10 },
                    { 19, 56455.5, "Ho hum", 11 },
                    { 20, 1454589.5, "Fiddle dee", 11 },
                    { 21, 583453.5, "Service ABC", 12 },
                    { 22, 567.5, "Fiddle dum", 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentTermsId",
                table: "Invoices",
                column: "PaymentTermsId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_VendorId",
                table: "Invoices",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicesLineItem_InvoiceId",
                table: "InvoicesLineItem",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicesLineItem");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "PaymentTerms");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
