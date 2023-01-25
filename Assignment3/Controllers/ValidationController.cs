using Assignment3.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Vendors.Entities;

namespace Assignment3.Controllers
{
    public class ValidationController : Controller
    {

        private VendorDbContext _vendorDbContext;

        public ValidationController(VendorDbContext vendorDbContext)
        {
            _vendorDbContext = vendorDbContext;
        }

        public IActionResult CheckPhoneNumber(string VendorPhone)
        {
            Console.WriteLine($"the input is ({VendorPhone})");
            string phoneCheckMsg = CheckIfPhoneExistsInDb(VendorPhone);
            if (string.IsNullOrEmpty(phoneCheckMsg))
            {
                // return (using temp data) an indication to the client that the email addr is valid
                TempData["okPhone"] = true;

                // plus return JSON true value:
                return Json(true);
            }
            else
            {
                // returning a msg indicating the address is already in use:
                return Json(phoneCheckMsg);
            }
        }

        private string CheckIfPhoneExistsInDb(string phone)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(phone))
            {
                var vendor = _vendorDbContext.Vendors.Where(v => v.VendorPhone == phone).FirstOrDefault();
                if (vendor != null)
                    msg = $"The phone number \"{phone}\" is already in use.";
            }

            return msg;
        }
    }
}
