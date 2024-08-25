namespace VMart.Services.EmailAPI.Models.DTOs
{
    public class SendDto
    {
        public string to { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
