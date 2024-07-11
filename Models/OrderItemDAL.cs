using ECommWeb.Entities;

namespace ECommWeb.Models
{
   
    public class OrderItemDAL
    {
        private readonly ApplicationDbContext db;
        public OrderItemDAL(ApplicationDbContext db)
        {
            
            this.db = db;
        }


        public int AddProduct(Product prod)
        {
            var result = 0;
            db.Products.Add(prod);


            result = db.SaveChanges();
            return result;
        }
    }

    
}
