using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendors.Entities;

namespace Vendors.Services
{
    public interface IInvoiceManager
    {
        public List<Invoice> GetInvoicesByVendorId(int vendorId);

        public Invoice? GetInvoiceById(int invoiceId);

        public List<InvoiceLineItem> GetInvoiceLinestemsByInvoiceId(int invoiceId);

        public int AddNewInvoice(Invoice invoice);

        public InvoiceLineItem AddNewInvoiceLineItem(InvoiceLineItem invoiceLineItem);

        public List<PaymentTerms> GetPaymentTerms();
    }
}
