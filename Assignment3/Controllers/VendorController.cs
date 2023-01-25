using Microsoft.AspNetCore.Mvc;
using Vendors.Entities;
using Vendors.Services;
using Assignment3.DataAccess;
using Assignment3.Models;
using Vendors.Service;
using System.Drawing;
using Assignment3.Components;
using System.Diagnostics;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Html;

namespace Assignment3.Controllers
{
    public class VendorController : Controller
    {

        private IVendorManager _vendorManager;
        private IInvoiceManager _invoiceManager;

        public VendorController(IVendorManager vendorManager, IInvoiceManager invoiceManager)
        {
            _vendorManager = vendorManager;
            _invoiceManager = invoiceManager;
        }


        [HttpGet("/vendors/groups/{lowerbound}-{upperbound}")]
        public IActionResult GetVendorsByGroup(string lowerBound, string upperbound)
        {
            List<Vendor> vendors = _vendorManager.GetVendorsByGroup(lowerBound, upperbound)
                .ToList();

            return View("Vendors", vendors);
        }


        [HttpGet("/vendors/groups/{lowerbound}-{upperbound}/{id}/invoices")]
        public IActionResult GetInvoicesByVendorId(int id)
        {
            InvoiceDetailsViewModel invoiceDetailsViewModel = new InvoiceDetailsViewModel()
            {
                ActiveVendor = _vendorManager.GetVendorById(id),
                NewInvoice = new Invoice(),
                InvoiceSelected = new Invoice(),
                PaymentTermsList = _invoiceManager.GetPaymentTerms()
            };

            invoiceDetailsViewModel.InvoiceSelected = (_vendorManager.GetVendorById(id)).Invoices[0];

            invoiceDetailsViewModel.ActiveVendor.Invoices = _invoiceManager.GetInvoicesByVendorId(id).ToList(); 

            return View("Invoice", invoiceDetailsViewModel);
        }

        [HttpGet("/vendors/groups/{lowerbound}-{upperbound}/{id}/invoices/{invoiceId}")]
        public IActionResult GetInvoiceLineItems(int id, int invoiceId)
        {
            InvoiceDetailsViewModel invoiceDetailsViewModel = new InvoiceDetailsViewModel()
            {
                ActiveVendor = _vendorManager.GetVendorById(id),
                PaymentTermsList = _invoiceManager.GetPaymentTerms(),
                NewInvoice = new Invoice(),
                InvoiceSelected = _invoiceManager.GetInvoiceById(invoiceId)
            };

            return View("Invoice", invoiceDetailsViewModel);
        }


        [HttpGet("/vendors/groups/{lowerbound}-{upperbound}/add-request")]
        public IActionResult GetAddVendorRequest()
        {
            Vendor vendor = new Vendor();

            return View("Add", vendor);
        }

        [HttpPost("/vendors/groups/{lowerbound}-{upperbound}/add-requests")]
        public IActionResult AddNewVendor(Vendor vendor, string lowerbound, string upperbound)
        {
            if (ModelState.IsValid)
            {
                _vendorManager.AddNewVendor(vendor);

                string[] letters = _vendorManager.GetGroupVendor(vendor.Name).Split('-');

                lowerbound = letters[0];
                upperbound = letters[1];

                TempData["LastActionMessage"] = $"The vendor \"{vendor.Name}\" was added.";

                return RedirectToAction("GetVendorsByGroup", "Vendor", new { lowerbound = lowerbound, upperbound = upperbound });
            }
            else
            {
                ModelState.AddModelError("", "There are errors in the form.");


                return View("Add", vendor);
            }
        }


        [HttpPost("/vendors/groups/{lowerbound}-{upperbound}/{id}/invoices")]
        public IActionResult AddInvoiceToVendorById(int id, InvoiceDetailsViewModel invoiceDetailsViewModel, string lowerbound, string upperbound)
        {
            invoiceDetailsViewModel.NewInvoice.VendorId = id;

            _invoiceManager.AddNewInvoice(invoiceDetailsViewModel.NewInvoice);
            
            Vendor vendor = new Vendor();
            vendor = _vendorManager.GetVendorById(id);

            string[] letters = _vendorManager.GetGroupVendor(vendor.Name).Split('-');
            
            lowerbound = letters[0];
            upperbound = letters[1];
            
            TempData["LastActionMessage"] = $"The invoice was added.";
            
            return RedirectToAction("GetInvoicesByVendorId", "Vendor", new { id = id, lowerbound = lowerbound, upperbound = upperbound});
        }

        [HttpPost("/vendors/groups/{lowerbound}-{upperbound}/{id}/invoices/{invoiceId}")]
        public IActionResult AddLineItemToInvoiceById(int id, int invoiceId, InvoiceLineItemsViewModel InvoiceLineItemsViewModel, string lowerbound, string upperbound)
        {
            if(InvoiceLineItemsViewModel.NewLineItem.Amount == null)
            {
                InvoiceLineItemsViewModel.NewLineItem.Amount = 0;
            }

            InvoiceLineItemsViewModel.NewLineItem.InvoiceId = invoiceId;
            _invoiceManager.AddNewInvoiceLineItem(InvoiceLineItemsViewModel.NewLineItem);

            Vendor vendor = new Vendor();
            vendor = _vendorManager.GetVendorById(id);

            string[] letters = _vendorManager.GetGroupVendor(vendor.Name).Split('-');

            lowerbound = letters[0];
            upperbound = letters[1];

            TempData["LastActionMessage"] = $"The line item was added.";

            return RedirectToAction("GetInvoicesByVendorId", "Vendor", new { id = id, lowerbound = lowerbound, upperbound = upperbound });
        }


        [HttpGet("/vendors/groups/{lowerbound}-{upperbound}/{id}/edit-request")]
        public IActionResult GetEditRequestById(int id)
        {
            Vendor vendor = new Vendor();
            vendor = _vendorManager.GetVendorById(id);

            return View("Edit", vendor);
        }

        [HttpPost("/vendors/groups/{lowerbound}-{upperbound}/{id}/edit-requests")]
        public IActionResult ProcessEditRequest(int id, Vendor vendor, string lowerbound, string upperbound)
        {
            if (ModelState.IsValid)
            {
                _vendorManager.EditVendor(vendor);

                string[] letters = _vendorManager.GetGroupVendor(vendor.Name).Split('-');

                lowerbound = letters[0];
                upperbound = letters[1];

                TempData["LastActionMessage"] = $"The vendor \"{vendor.Name}\" was updated.";

                return RedirectToAction("GetVendorsByGroup", "Vendor", new {lowerbound = lowerbound, upperbound = upperbound});
            }
            else
            {
                ModelState.AddModelError("", "There are errors in the form.");

                return View("Edit", vendor);
            }
        }



        [HttpGet("/vendors/groups/{lowerbound}-{upperbound}/{id}/delete")]
        public IActionResult ProcessDeleteRequestById(int id, string lowerbound, string upperbound)
        {
            Vendor vendor = new Vendor();
            vendor = _vendorManager.GetVendorById(id);

            string[] letters = _vendorManager.GetGroupVendor(vendor.Name).Split('-');

            lowerbound = letters[0];
            upperbound = letters[1];

            vendor.IsDeleted = true;
            _vendorManager.EditVendor(vendor);

            string url = $"https://localhost:7255/vendors/groups/{lowerbound}-{upperbound}/{id}/undo";

            TempData["LastActionMessageUndo"] = $"The vendor \"{vendor.Name}\" was deleted. <a href=\"{url}\">Undo</a> the action";

            return RedirectToAction("GetVendorsByGroup", "Vendor", new { lowerbound = lowerbound, upperbound = upperbound });
        }

        [HttpGet("/vendors/groups/{lowerbound}-{upperbound}/{id}/undo")]
        public IActionResult UndoDelete(int id, string lowerbound, string upperbound)
        {
            Vendor vendor = new Vendor();
            vendor = _vendorManager.GetVendorById(id);

            string[] letters = _vendorManager.GetGroupVendor(vendor.Name).Split('-');

            lowerbound = letters[0];
            upperbound = letters[1];

            vendor.IsDeleted = false;
            _vendorManager.EditVendor(vendor);

            return RedirectToAction("GetVendorsByGroup", "Vendor", new { lowerbound = lowerbound, upperbound = upperbound });
        }
    }
}
