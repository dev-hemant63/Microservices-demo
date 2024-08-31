using VMart.WebApp.Models;
using VMart.WebApp.Models.Dto;

namespace VMart.WebApp.Services.IServices
{
    public interface IOrderService
    {
        Task<ResponseDto<object>> Place(OrderDto orderDto);
        Task<ResponseDto<List<Orders>>> GetAsync();
        Task<ResponseDto<List<OrderDetails>>> GetDetailsAsync(int orderId);
    }
}
