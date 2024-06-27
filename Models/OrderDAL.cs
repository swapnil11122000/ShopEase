using ECommWeb.Entities;

namespace ECommWeb.Models
{
    public class OrderDAL
    {
        private readonly ApplicationDbContext _context;
        public OrderDAL(ApplicationDbContext  _Context)
        {
                this._context = _Context;
        }


    }
}
