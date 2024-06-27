using ECommWeb.Entities;

namespace ECommWeb.Models
{
    public class Order_ItemDAL
    {
        private readonly ApplicationDbContext _context;
        public Order_ItemDAL(ApplicationDbContext _Context)
        {
            this._context = _Context;
        }

    }
}
