using System.ComponentModel.DataAnnotations;

namespace VMart.Services.Category.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
