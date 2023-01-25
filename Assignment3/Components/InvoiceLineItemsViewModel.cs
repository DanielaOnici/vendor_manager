using Vendors.Entities;

namespace Assignment3.Components
{
    public class InvoiceLineItemsViewModel
    {

        public Invoice ActiveInvoice { get; set; }

        public List<InvoiceLineItem> InvoiceLineItemsList { get; set;}

        public InvoiceLineItem NewLineItem { get; set; }
    }
}
