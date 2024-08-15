namespace VMart.WebApp.Services.IServices
{
    public interface ITokenProvider
    {
        Task<string> GetToken();
    }
}
