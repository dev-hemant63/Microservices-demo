using VMart.WebApp.Models.Dto;

namespace VMart.WebApp.Services.IServices
{
    public interface IOrderService
    {
        Task<ResponseDto<object>> Place(OrderDto orderDto);
    }
}
