using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendors.Entities
{
    public class Vendor
    {
        public int VendorId { get; set; }


        [Required (ErrorMessage = "Please, insert a name.")]
        public string Name { get; set; } = null!;


        [Required (ErrorMessage = "Please, insert an address.")]
        public string? Address1 { get; set; }


        public string? Address2 { get; set; }


        [Required (ErrorMessage = "Please, insert the city.")]
        public string? City { get; set; } = null!;


        [Required (ErrorMessage = "Please, insert the province/state")]
        [RegularExpression(@"^[A-Za-z]{2}$", ErrorMessage = "Please, insert a valid Province/State with only 2 letters")]
        public string? ProvinceOrState { get; set; } = null!;


        [Required (ErrorMessage = "Please, insert the zip/postal code")]
        [RegularExpression(@"^\d{5}|[A-Za-z]{1}\d{1}[A-Za-z]{1}\s?\d{1}[A-Za-z]{1}\d{1}$", ErrorMessage = "Please, insert a valid zip/postal code")]
        public string? ZipOrPostalCode { get; set; } = null!;

        [Required (ErrorMessage = "Please, insert the phone number")]
        [Remote("CheckPhoneNumber", "Validation")]
        [RegularExpression(@"^[0-9]{3}-[0-9]{3}-[0-9]{4}$", ErrorMessage = "Please, follow the pattern 000-000-0000")]
        public string? VendorPhone { get; set; }

        public string? VendorContactLastName { get; set; }

        public string? VendorContactFirstName { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please, insert a valid email")]
        public string? VendorContactEmail { get; set; }

        public bool IsDeleted { get; set; } = false;

        public List<Invoice>? Invoices { get; set; }
    }
}
