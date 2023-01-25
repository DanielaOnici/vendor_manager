using Microsoft.EntityFrameworkCore;
using Vendors.Entities;
using Vendors.Services;
using Assignment3.DataAccess;
using Vendors.Service;

namespace Assignment3.Services
{
    public class VendorManager : IVendorManager
    {
        private VendorDbContext _vendorDbContext;

        public VendorManager(VendorDbContext vendorDbContext)
        {
            _vendorDbContext = vendorDbContext;
        }

        public List<Vendor> GetVendorsByGroup(string lowerBound, string upperBound)
        {
            return GetBaseQuery()
                    .Where(v => v.Name.ToLower().Substring(0,1).CompareTo(lowerBound) >= 0
                    && v.Name.ToLower().Substring(0,1).CompareTo(upperBound) <= 0)
                    .Where(v => v.IsDeleted == false)
                    .OrderBy(v => v.Name)
                    .ToList();
        }

        // Add a new movie and returns its int ID:
        public int AddNewVendor(Vendor vendor)
        {
            _vendorDbContext.Vendors.Add(vendor);
            _vendorDbContext.SaveChanges();
            return vendor.VendorId;
        }

        public Vendor? GetVendorById(int id)
        {
            return GetBaseQuery()
                    .Where(v => v.VendorId == id)
                    .FirstOrDefault();
        }

        public void EditVendor(Vendor vendor)
        {
            _vendorDbContext.Vendors.Update(vendor);
            _vendorDbContext.SaveChanges();
        }

        public void DeleteVendorById(int id)
        {
            var vendor = _vendorDbContext.Vendors.Find(id);
            _vendorDbContext.Vendors.Remove(vendor);
            _vendorDbContext.SaveChanges();
        }

        private IQueryable<Vendor> GetBaseQuery()
        {
            return _vendorDbContext.Vendors
                .Include(v => v.Invoices)
                .ThenInclude(i => i.InvoiceLineItems)
                .Include(v => v.Invoices)
                .ThenInclude(i => i.PaymentTerms);
        }

        public string GetGroupVendor(string vendor)
        {

            if(vendor.ToUpper().StartsWith("A") || vendor.ToUpper().StartsWith("B") || vendor.ToUpper().StartsWith("C") || vendor.ToUpper().StartsWith("D") || vendor.ToUpper().StartsWith("E"))
            {
                return "a-e";
            }
            else if(vendor.ToUpper().StartsWith("F") || vendor.ToUpper().StartsWith("G") || vendor.ToUpper().StartsWith("H") || vendor.ToUpper().StartsWith("I") || vendor.ToUpper().StartsWith("J") || vendor.ToUpper().StartsWith("K"))
            {
                return "f-k";
            }
            else if(vendor.ToUpper().StartsWith("L") || vendor.ToUpper().StartsWith("M") || vendor.ToUpper().StartsWith("N") || vendor.ToUpper().StartsWith("O") || vendor.ToUpper().StartsWith("P") || vendor.ToUpper().StartsWith("Q") || vendor.ToUpper().StartsWith("R"))
            {
                return "l-r";
            }
            else
            {
                return "s-z";
            }
            
        }
    }
}
