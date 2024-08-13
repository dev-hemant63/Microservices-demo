using System.ComponentModel.DataAnnotations;

namespace VMart.Services.ProductAPI.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Product_Name { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
    }
}
