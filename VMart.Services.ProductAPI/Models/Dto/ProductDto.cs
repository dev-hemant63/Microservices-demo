namespace VMart.Services.ProductAPI.Models.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Product_Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile ProductImage { get; set; }
        public string Description { get; set; }
    }
}
