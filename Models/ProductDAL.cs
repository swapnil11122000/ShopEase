using ECommWeb.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ECommWeb.Models
{
    public class ProductDAL
    {
        private readonly ApplicationDbContext db;

        public ProductDAL(ApplicationDbContext db)
        {
            this.db = db;
        }




        // display all
        public List<Product> GetAllProducts()
        {

            return db.Products.Include(p => p.Category).ToList();
        }
        // display by id
        public Product GetProductById(int Id)
        {
            var result = db.Products.Where(x => x.ProductID == Id).FirstOrDefault();
            return result;
        }
        public Product SearchProductByName(string product)
        {
            var result = db.Products.Where(x => x.ProductName == product).FirstOrDefault();
            return result;
        }


        // add record
        public int AddProduct(Product prod)
        {     
            var result = 0;
            db.Products.Add(prod);
          
           
            result = db.SaveChanges();
            return result;
        }
        //update record
        public int UpdateProduct(Product prod)
        {
            int result = 0;
            var res = db.Products.Where(x => x.ProductID == prod.ProductID).FirstOrDefault();
            if (res != null)
            {
                res.ProductName = prod.ProductName;
              
                res.UnitPrice = prod.UnitPrice;
              
                result = db.SaveChanges();
            }
            return result;
        }
        // delete record
        public int DeleteProduct(int Id)
        {
            int res = 0;
            var result = db.Products.Where(x => x.ProductID == Id).FirstOrDefault();
            if (result != null)
            {
                db.Products.Remove(result);
                res = db.SaveChanges();
            }
            return res;
        }

        public void InsertItemToCart(Cart cart)
        {

            db.Carts.Add(cart);

            db.SaveChanges();

        }
    }
}
