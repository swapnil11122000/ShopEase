using ECommWeb.Entities;
using Microsoft.CodeAnalysis;
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

        public List<Product> GetAllProducts()
        {

            return db.Products.Include(p => p.Category).ToList();
        }
      
        public Product GetProductById(int Id)
        {
            var query = from product in db.Products
                        join category in db.Category on product.CategoryID equals category.CategoryID
                        where product.ProductID == Id
                        select new Product
                        {
                            ProductID = product.ProductID,
                            ProductName = product.ProductName,
                            Description = product.Description,
                            UnitPrice = product.UnitPrice,
                            Category = category,
                            ShortText=product.ShortText,
                            ImgUrl=product.ImgUrl,
                            StockQuantity=product.StockQuantity,
                            BarCode=product.BarCode,
                            VendorID=product.VendorID,
                            
                        };

            return query.FirstOrDefault();
        }
        public List<Product> SearchProductByName(string product)
        {
            var query = from p in db.Products
                        join c in db.Category on p.CategoryID equals c.CategoryID
                        where p.ProductName.Contains(product)
                        select new Product
                        {
                           
                            ProductID = p.ProductID,
                            ProductName = p.ProductName,
                         UnitPrice = p.UnitPrice,
                          ImgUrl = p.ImgUrl,
                          VendorID = p.VendorID,
                          CategoryID = p.CategoryID,
                            Category = new Category
                            {
                                CategoryID = c.CategoryID,
                                CategoryName = c.CategoryName
                                
                            }
                          
                        };

            return query.ToList();
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
