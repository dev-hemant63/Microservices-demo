namespace VMart.WebApp.Models
{
    public class Orders
    {
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
