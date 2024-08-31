namespace VMart.WebApp.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public DateTime EntryAt { get; set; }
        public virtual Orders Order { get; set; }
    }
}
