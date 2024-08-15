using VMart.WebApp.Utility;

namespace VMart.WebApp.Models.Dto
{
    public class RequestDto
    {
        public string Url { get; set; }
        public string Token { get; set; }
        public Dictionary<string,string> Header { get; set; }
        public object RequestBody { get; set; }
        public RequestType RequestType { get; set; }
    }
}
