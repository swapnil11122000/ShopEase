using System.ComponentModel.DataAnnotations;

namespace ECommWeb.Models
{
    public class Category
    {
        [Key]
        [ScaffoldColumn(false)]
        public int CategoryID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
