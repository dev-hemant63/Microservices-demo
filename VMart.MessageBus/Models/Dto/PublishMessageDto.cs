namespace VMart.MessageBus.Models.Dto
{
    public class PublishMessageDto
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public string Queue { get; set; }
        public string Message { get; set; }
    }
}
