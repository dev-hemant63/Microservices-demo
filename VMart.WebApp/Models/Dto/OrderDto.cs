namespace VMart.WebApp.Models.Dto
{
    public class OrderDto
    {
        public List<OrderDetailDto> Products { get; set; }
    }
    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }
}
