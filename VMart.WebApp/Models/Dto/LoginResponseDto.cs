namespace VMart.WebApp.Models.Dto
{
    public class LoginResponseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<string> Role { get; set; } = new List<string>();
        public string Token { get; set; }
    }
}
