using ECommWeb.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace ECommWeb.Models
{
    public class VendorDAL
    {
        private readonly ApplicationDbContext _context;

        public VendorDAL(ApplicationDbContext _Context)
        {
              this._context=_Context;
        }

        public List<Vendor> GetAllVendor()
        {
            return _context.Vendor.ToList();
        }

        public Vendor GetVendorByID(int id)
        {
            var result=_context.Vendor.FirstOrDefault(x => x.VendorID == id);
            return result;
        }

        public int AddVendor(Vendor Vendor)
        {
            int result=0;
            _context.Vendor.Add(Vendor);
            result=_context.SaveChanges();
            return result;
        }
       
        public int UpdateVendor(Vendor Vendor)
        {
            int result = 0;
            var res = _context.Vendor.Where(x => x.VendorID == Vendor.VendorID).FirstOrDefault();
            if (res != null)
            {
                res.ContactPerson = Vendor.ContactPerson;
                res.Phone = Vendor.Phone;
                res.Email = Vendor.Email;
                res.AddressID = Vendor.AddressID;
              

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
            var result= _context.Vendor.Where(x=>x.VendorID==vendorID).FirstOrDefault();
            if(result != null)
            {
                _context.Vendor.Remove(result);
                res= _context.SaveChanges();
            }
            return res;

        }
    }
}
