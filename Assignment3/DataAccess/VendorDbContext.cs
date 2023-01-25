using Microsoft.EntityFrameworkCore;
using Vendors.Entities;


namespace Assignment3.DataAccess
{
    public class VendorDbContext : DbContext
    {
        public VendorDbContext(DbContextOptions<VendorDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceLineItem> InvoicesLineItem { get; set; }

        public DbSet<PaymentTerms> PaymentTerms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // setup 1-to-many between vendor & invoice:
            modelBuilder.Entity<Invoice>().HasOne(i => i.Vendor).WithMany(v => v.Invoices).HasForeignKey(i => i.VendorId);

            // setup 1-to-many between payment terms & invoices:
            modelBuilder.Entity<Invoice>().HasOne(i => i.PaymentTerms).WithMany(v => v.Invoices).HasForeignKey(i => i.PaymentTermsId);

           


            //seed payment terms
            modelBuilder.Entity<PaymentTerms>().HasData(
                new PaymentTerms() { PaymentTermsId = 1, Description = "Net due 10 days", DueDays = 10 },
                new PaymentTerms() { PaymentTermsId = 2, Description = "Net due 20 days", DueDays = 20 },
                new PaymentTerms() { PaymentTermsId = 3, Description = "Net due 30 days", DueDays = 30 },
                new PaymentTerms() { PaymentTermsId = 4, Description = "Net due 60 days", DueDays = 60 },
                new PaymentTerms() { PaymentTermsId = 5, Description = "Net due 90 days", DueDays = 90 }
                );



            // seed vendor
            modelBuilder.Entity<Vendor>().HasData(
                new Vendor()
                {
                    VendorId = 1,
                    Name = "Blanchard & Johnson Associates",
                    Address1 = "27371 Valderas",
                    City = "Mission Viejo",
                    ProvinceOrState = "CA",
                    ZipOrPostalCode = "92691",
                    VendorPhone = "214-555-3647",
                    VendorContactEmail = "kgonz@bja.com",
                    VendorContactFirstName = "Keeton",
                    VendorContactLastName = "Gonzalo"
                },
                new Vendor()
                {
                    VendorId = 2,
                    Name = "California Chamber Of Commerce",
                    Address1 = "3255 Ramos Cir",
                    City = "Sacramento",
                    ProvinceOrState = "CA",
                    ZipOrPostalCode = "95827",
                    VendorPhone = "916-555-6670",
                    VendorContactEmail = "manton@gmail.com",
                    VendorContactFirstName = "Mauro",
                    VendorContactLastName = "Anton"
                },
                new Vendor()
                {
                    VendorId = 3,
                    Name = "Golden Eagle Insurance Co",
                    Address1 = "PO Box 85826",
                    City = "San Diego",
                    ProvinceOrState = "CA",
                    ZipOrPostalCode = "92186",
                    VendorPhone = "917-544-7090",
                    VendorContactFirstName = "Jane",
                    VendorContactLastName = "Smith"
                },
                new Vendor()
                {
                    VendorId = 4,
                    Name = "Fresno Photoengraving Company",
                    Address1 = "1952 H Street",
                    Address2 = "P.O. Box 1952",
                    City = "Fresno",
                    ProvinceOrState = "CA",
                    ZipOrPostalCode = "93718",
                    VendorPhone = "559-555-3005",
                    VendorContactFirstName = "Chad",
                    VendorContactLastName = "Jones"
                },
                new Vendor()
                {
                    VendorId = 5,
                    Name = "Nielson",
                    Address1 = "Ohio Valley Litho Division",
                    City = "Cincinnate",
                    ProvinceOrState = "OH",
                    ZipOrPostalCode = "45264",
                    VendorPhone = "519-824-3477",
                    VendorContactFirstName = "Paul",
                    VendorContactLastName = "Morgan"
                },
                new Vendor()
                {
                    VendorId = 6,
                    Name = "Naylor Publications Inc",
                    Address1 = "PO Box 40513",
                    City = "Jacksonville",
                    ProvinceOrState = "FL",
                    ZipOrPostalCode = "32231",
                    VendorPhone = "800-555-6041",
                    VendorContactEmail = "gerald.kristoff@outlook.com",
                    VendorContactFirstName = "Gerald",
                    VendorContactLastName = "Kristoff"
                },
                new Vendor()
                {
                    VendorId = 7,
                    Name = "Venture Communications",
                    Address1 = "60 Madison Ave",
                    City = "New York",
                    ProvinceOrState = "NY",
                    ZipOrPostalCode = "10010",
                    VendorPhone = "212-533-4800",
                    VendorContactEmail = "tneftaly@venture.com",
                    VendorContactFirstName = "Thalia",
                    VendorContactLastName = "Neftaly"
                },
                new Vendor()
                {
                    VendorId = 8,
                    Name = "US Postal Service",
                    Address1 = "Attn:  Supt. Window Services",
                    Address2 = "PO Box 7005",
                    City = "Madison",
                    ProvinceOrState = "WI",
                    ZipOrPostalCode = "53707",
                    VendorPhone = "800-555-1205",
                    VendorContactFirstName = "Alberto",
                    VendorContactLastName = "Francesco"
                }
                );



            //seed Invoice
            modelBuilder.Entity<Invoice>().HasData(
                new Invoice() { InvoiceId = 1, InvoiceDate = new DateTime(2022, 8, 5), PaymentTermsId = 3, VendorId = 1 },
                new Invoice() { InvoiceId = 2, InvoiceDate = new DateTime(2022, 8, 17), PaymentTermsId = 3, VendorId = 1 },
                new Invoice() { InvoiceId = 3, InvoiceDate = new DateTime(2022, 9, 7), PaymentTermsId = 3, VendorId = 2 },
                new Invoice() { InvoiceId = 4, InvoiceDate = new DateTime(2022, 9, 28), PaymentTermsId = 4, VendorId = 2 },
                new Invoice() { InvoiceId = 5, InvoiceDate = new DateTime(2022, 10, 8), PaymentTermsId = 3, VendorId = 3 },
                new Invoice() { InvoiceId = 6, InvoiceDate = new DateTime(2022, 10, 31), PaymentTermsId = 4, VendorId = 3 },
                new Invoice() { InvoiceId = 7, InvoiceDate = new DateTime(2022, 11, 11), PaymentTermsId = 3, VendorId = 4 },
                new Invoice() { InvoiceId = 8, InvoiceDate = new DateTime(2022, 11, 12), PaymentTermsId = 5, VendorId = 4 },
                new Invoice() { InvoiceId = 9, InvoiceDate = new DateTime(2022, 11, 24), PaymentTermsId = 3, VendorId = 5 },
                new Invoice() { InvoiceId = 10, InvoiceDate = new DateTime(2022, 12, 8), PaymentTermsId = 3, VendorId = 6 },
                new Invoice() { InvoiceId = 11, InvoiceDate = new DateTime(2022, 12, 21), PaymentTermsId = 2, VendorId = 7 },
                new Invoice() { InvoiceId = 12, InvoiceDate = new DateTime(2022, 12, 23), PaymentTermsId = 3, VendorId = 8 });



            //seed invoice line item
            modelBuilder.Entity<InvoiceLineItem>().HasData(
                new InvoiceLineItem() { InvoiceLineItemId = 1, Description = "Part 123", Amount = 1089.99, InvoiceId = 1 },
                new InvoiceLineItem() { InvoiceLineItemId = 2, Description = "Service XYZ", Amount = 173499.5, InvoiceId = 1 },
                new InvoiceLineItem() { InvoiceLineItemId = 3, Description = "Part 110", Amount = 4654499.5, InvoiceId = 2 },
                new InvoiceLineItem() { InvoiceLineItemId = 4, Description = "Part A", Amount = 78799.5, InvoiceId = 2 },
                new InvoiceLineItem() { InvoiceLineItemId = 5, Description = "Part AA", Amount = 679.5, InvoiceId = 3 },
                new InvoiceLineItem() { InvoiceLineItemId = 6, Description = "Part Z", Amount = 786.9, InvoiceId = 3 },
                new InvoiceLineItem() { InvoiceLineItemId = 7, Description = "Trip 1", Amount = 998.5, InvoiceId = 4 },
                new InvoiceLineItem() { InvoiceLineItemId = 8, Description = "Service XYZ", Amount = 1011.5, InvoiceId = 4 },
                new InvoiceLineItem() { InvoiceLineItemId = 9, Description = "Rental DEF", Amount = 565735.5, InvoiceId = 5 },
                new InvoiceLineItem() { InvoiceLineItemId = 10, Description = "Rental ZZZ", Amount = 5753.5, InvoiceId = 5 },
                new InvoiceLineItem() { InvoiceLineItemId = 11, Description = "Service ABC", Amount = 58453.9, InvoiceId = 6 },
                new InvoiceLineItem() { InvoiceLineItemId = 12, Description = "Service ABC", Amount = 111232.5, InvoiceId = 6 },
                new InvoiceLineItem() { InvoiceLineItemId = 13, Description = "Rental ABC", Amount = 109.5, InvoiceId = 7 },
                new InvoiceLineItem() { InvoiceLineItemId = 14, Description = "Rental ABC", Amount = 57846.5, InvoiceId = 8 },
                new InvoiceLineItem() { InvoiceLineItemId = 15, Description = "Trip 2", Amount = 132.5, InvoiceId = 9 },
                new InvoiceLineItem() { InvoiceLineItemId = 16, Description = "Service XYZ", Amount = 6877.9, InvoiceId = 9 },
                new InvoiceLineItem() { InvoiceLineItemId = 17, Description = "Trip 3", Amount = 8999.5, InvoiceId = 10 },
                new InvoiceLineItem() { InvoiceLineItemId = 18, Description = "Blah blah", Amount = 1033.5, InvoiceId = 10 },
                new InvoiceLineItem() { InvoiceLineItemId = 19, Description = "Ho hum", Amount = 56455.5, InvoiceId = 11 },
                new InvoiceLineItem() { InvoiceLineItemId = 20, Description = "Fiddle dee", Amount = 1454589.5, InvoiceId = 11 },
                new InvoiceLineItem() { InvoiceLineItemId = 21, Description = "Service ABC", Amount = 583453.5, InvoiceId = 12 },
                new InvoiceLineItem() { InvoiceLineItemId = 22, Description = "Fiddle dum", Amount = 567.5, InvoiceId = 12 }
                );

        }
    }
}
