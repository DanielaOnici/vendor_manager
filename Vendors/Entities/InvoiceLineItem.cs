namespace Vendors.Entities
{
    public class InvoiceLineItem
    {
        public int InvoiceLineItemId { get; set; }

        public double Amount { get; set; }

        public string? Description { get; set; }

        // FK:
        public int? InvoiceId { get; set; }

        // Nav prop to invoice:
        public Invoice? Invoice { get; set; }
    }
}
