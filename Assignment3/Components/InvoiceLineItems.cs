using Microsoft.AspNetCore.Mvc;
using Vendors.Entities;
using Vendors.Service;
using Vendors.Services;

namespace Assignment3.Components
{
    public class InvoiceLineItems : ViewComponent
    {
        private IVendorManager _vendorManager;
        private IInvoiceManager _invoiceManager;

        public InvoiceLineItems(IVendorManager vendorManager, IInvoiceManager invoiceManager)
        {
            _vendorManager = vendorManager;
            _invoiceManager = invoiceManager;
        }

        public IViewComponentResult Invoke(int id, int invoiceId, string lowerbound, string upperbound)
        {
            InvoiceLineItemsViewModel invoiceLineitemViewModel = new InvoiceLineItemsViewModel()
            {
                ActiveInvoice = _invoiceManager.GetInvoiceById(invoiceId),
                InvoiceLineItemsList = _invoiceManager.GetInvoiceLinestemsByInvoiceId(invoiceId),
                NewLineItem = new InvoiceLineItem() 
            };
            
            return View(invoiceLineitemViewModel);
        }
    }
}
