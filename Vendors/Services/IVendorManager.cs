using Vendors.Entities;


namespace Vendors.Service
{
    public interface IVendorManager
    {
        public List<Vendor> GetVendorsByGroup(string lowerBound, string upperBound);

        // Add a new movie and returns its int ID:
        public int AddNewVendor(Vendor vendor);

        public Vendor? GetVendorById(int id);

        public void EditVendor(Vendor vendor);

        public void DeleteVendorById(int id);

        public string GetGroupVendor(string vendor);
    }
}
