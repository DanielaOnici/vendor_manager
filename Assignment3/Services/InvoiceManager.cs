using Microsoft.EntityFrameworkCore;
using Vendors.Entities;
using Vendors.Services;
using Assignment3.DataAccess;
using System.Numerics;

namespace Assignment3.Services
{
    public class InvoiceManager : IInvoiceManager
    {

        private VendorDbContext _vendorDbContext;

        public InvoiceManager(VendorDbContext vendorDbContext)
        {
            _vendorDbContext = vendorDbContext;
        }

        public List<Invoice> GetInvoicesByVendorId(int vendorId)
        {
            var invoices = _vendorDbContext.Invoices
                    .Where(i => i.VendorId == vendorId)
                    .OrderBy(i => i.InvoiceDate)
                    .ToList();

            return invoices;
        }

        public Invoice? GetInvoiceById(int invoiceId)
        {
            return GetBaseQuery()
                    .Where(i => i.InvoiceId == invoiceId)
                    .FirstOrDefault();
        }

        public List<InvoiceLineItem> GetInvoiceLinestemsByInvoiceId(int invoiceId)
        {
            return _vendorDbContext.InvoicesLineItem
                    .Include(i => i.Invoice)
                    .Where(i => i.InvoiceId == invoiceId)
                    .ToList();
        }

        public int AddNewInvoice(Invoice invoice)
        {
            _vendorDbContext.Invoices.Add(invoice);
            _vendorDbContext.SaveChanges();
            return invoice.InvoiceId;
        }

        public InvoiceLineItem AddNewInvoiceLineItem(InvoiceLineItem invoiceLineItem)
        {
            _vendorDbContext.InvoicesLineItem.Add(invoiceLineItem);
            _vendorDbContext.SaveChanges();
            return invoiceLineItem;
        }

        public List<PaymentTerms> GetPaymentTerms()
        {
            return _vendorDbContext.PaymentTerms
                .OrderBy(p => p.PaymentTermsId)
                .ToList();
        }

        private IQueryable<Invoice> GetBaseQuery()
        {
            return _vendorDbContext.Invoices
                .Include(i => i.Vendor)
                .Include(i => i.InvoiceLineItems)
                .Include(i => i.PaymentTerms);
        }
    }
}
