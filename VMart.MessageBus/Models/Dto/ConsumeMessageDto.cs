namespace VMart.MessageBus.Models.Dto
{
    public class ConsumeMessageDto
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public string Queue { get; set; }
    }
}
