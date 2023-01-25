using Vendors.Entities;

namespace Assignment3.Models
{
    public class InvoiceDetailsViewModel : AbstractBaseViewModel
    {
        public Invoice NewInvoice { get; set; }

        public List<PaymentTerms> PaymentTermsList { get; set; }

        public Invoice InvoiceSelected { get; set; }
    }
}
