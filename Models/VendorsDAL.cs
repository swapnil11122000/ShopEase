using ECommWeb.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace ECommWeb.Models
{
    public class VendorsDAL
    {
        private readonly ApplicationDbContext _context;

        public VendorsDAL(ApplicationDbContext _Context)
        {
              this._context=_Context;
        }

        public List<Vendors> GetAllVendors()
        {
            return _context.Vendors.ToList();
        }

        public Vendors GetVendorByID(int id)
        {
            var result=_context.Vendors.FirstOrDefault(x => x.Vendor_ID == id);
            return result;
        }

        public int AddVendor(Vendors vendors)
        {
            int result=0;
            _context.Vendors.Add(vendors);
            result=_context.SaveChanges();
            return result;
        }
       
        public int UpdateVendor(Vendors vendors)
        {
            int result = 0;
            var res = _context.Vendors.Where(x => x.Vendor_ID == vendors.Vendor_ID).FirstOrDefault();
            if (res != null)
            {
                res.Vendor_Name = vendors.Vendor_Name;
                res.Phone_Number = vendors.Phone_Number;
                res.Email = vendors.Email;
                res.Address1 = vendors.Address1;
                res.Address2 = vendors.Address2;

                result = _context.SaveChanges();
            }
            return result;
        }
        public DataTable GetVendorProducts()
        {
            DataTable result = new DataTable();

            return result;
        }
        public int DeleteVendor(int vendorID)
        {
            int res=0;
            var result= _context.Vendors.Where(x=>x.Vendor_ID==vendorID).FirstOrDefault();
            if(result != null)
            {
                _context.Vendors.Remove(result);
                res= _context.SaveChanges();
            }
            return res;

        }
    }
}
