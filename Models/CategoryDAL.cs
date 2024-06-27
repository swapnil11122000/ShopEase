using ECommWeb.Entities;

namespace ECommWeb.Models
{
    public class CategoryDAL
    {
        private readonly ApplicationDbContext _context;
        public CategoryDAL(ApplicationDbContext _Context)
        {
                this._context = _Context;
        }
    }
}
