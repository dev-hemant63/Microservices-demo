namespace VMart.WebApp.Models
{
    public class Products
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Product_Name { get; set; }
        public decimal Price { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
    }
}
