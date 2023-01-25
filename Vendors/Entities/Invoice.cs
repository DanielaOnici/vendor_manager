using System.ComponentModel.DataAnnotations;

namespace Vendors.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        [Required(ErrorMessage = "Please select the date")]
        public DateTime? InvoiceDate { get; set; }

        public DateTime? InvoiceDueDate
        {
            get
            {
                return InvoiceDate?.AddDays(Convert.ToDouble(PaymentTerms?.DueDays));
            }
        }

        public double? PaymentTotal { get; set; } = 0.0;

        public DateTime? PaymentDate { get; set; }


        [Required(ErrorMessage = "Please specify the payment term.")]
        public int PaymentTermsId { get; set; }

        // Nav to terms:
        public PaymentTerms? PaymentTerms { get; set; }

        // FK:
        public int VendorId { get; set; }

        // Nav to vendor
        public Vendor? Vendor { get; set; }

        // Nav to line items:
        public List<InvoiceLineItem>? InvoiceLineItems { get; set; }

        public string Total 
        { 
            get 
            {
                if(InvoiceLineItems.Count == 0)
                {
                    return 0.ToString("C");
                }
                else
                {
                    return (InvoiceLineItems.Sum(x => x.Amount)).ToString("C");
                }
            } 
        }
    }
}
