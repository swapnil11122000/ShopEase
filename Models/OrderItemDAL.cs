using ECommWeb.Entities;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace ECommWeb.Models
{

    public class OrderItemDAL
    {
        private readonly ApplicationDbContext db;
        public OrderItemDAL(ApplicationDbContext db)
        {

            this.db = db;
        }


        public int AddOrder(Product prod, int UserID)
        {
            Order order = new Order();
            order.UserID = UserID;
            order.OrderDate = DateTime.Now;
            order.TotalAmount = prod.OrderItem.TotalPrice;
            order.Status = "Placed";
            db.Order.Add(order);
            db.SaveChanges();

            return order.OrderID;
        }
        public int SubtractQuantity(int Quantity,Product product)
        {
            var productToUpdate = db.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
            productToUpdate.StockQuantity -= Quantity;
            int Result=db.SaveChanges();
            return Result;
        }
        public int AddOrderItem(Product prod, int OrderID)
        {
            OrderItem orderitem = new OrderItem();

            orderitem.OrderID = OrderID;
            orderitem.ProductID = prod.ProductID;
            orderitem.Quantity = prod.OrderItem.Quantity;
            orderitem.TotalPrice = prod.OrderItem.TotalPrice;

            db.OrderItem.Add(orderitem);
            db.SaveChanges();

            return orderitem.OrderID;

        }

        public List<OrderItem> GetMyOrders(int userID)
        {
            //var query = from order in db.Order
            //           join orderItem in db.OrderItem on order.OrderID equals orderItem.OrderID
            //           join product in db.Products on orderItem.ProductID equals product.ProductID
            //           where order.UserID == userID
            //           select new OrderItem
            //           {
            //               Order = order, 
            //              Product = product,
            //               Quantity = orderItem.Quantity,
            //               TotalPrice = orderItem.TotalPrice
            //           };
            var query = from order in db.Order
                        join orderItem in db.OrderItem on order.OrderID equals orderItem.OrderID
                        join product in db.Products on orderItem.ProductID equals product.ProductID
                        where order.UserID == userID
                        group new { orderItem, product } by order.OrderID into grouped
                        select new OrderItem
                        {
                            Order = grouped.FirstOrDefault().orderItem.Order,
                            Product = grouped.FirstOrDefault().product,
                            Quantity = grouped.Sum(x => x.orderItem.Quantity),
                            TotalPrice = grouped.Sum(x => x.orderItem.TotalPrice)
                        };



            return query.ToList();
        }


        

    }


}
