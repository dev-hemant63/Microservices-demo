namespace VMart.WebApp.Models
{
    public class ProductAddOrEditVM
    {
        public Products Products { get; set; }
        public IEnumerable<ProductCategory> ProductCategory { get; set; }
    }
}
