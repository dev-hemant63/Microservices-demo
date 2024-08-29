using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VMart.MessageBus.Services;
using VMart.MessageBus.Services.IService;
using VMart.Services.Order.Data;
using VMart.Services.Order.Extention;
using VMart.Services.Order.Models;
using VMart.Services.Order.Models.Dto;

namespace VMart.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly IMessageBus _messageBus;
        public OrderController(AppDbContext appDbContext, IMapper mapper, IMessageBus messageBus)
        {
            _appDbContext = appDbContext;
            _response = new ResponseDto { Message = "Sorry,Somethig went wrong try after sometime."};
            _mapper = mapper;
            _messageBus = messageBus;
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderDto orderDto)
        {
            try
            {
                var orderId = Guid.NewGuid().ToString();
                await _appDbContext.Orders.AddAsync(new Orders
                {
                    OrderDate = DateTime.Now,
                    OrderNo = orderId,
                    UserId = User.GetLogingId<int>(),
                });
                _appDbContext.SaveChangesAsync();
                var order = _appDbContext.Orders.Where(x => x.OrderId.Equals(orderId)).FirstOrDefault();
                if (order != null)
                {
                    var orderDetails = _mapper.Map<List<OrderDetails>>(orderDto.Products);
                    await _appDbContext.OrderDetails.AddRangeAsync(orderDetails);
                    await _appDbContext.SaveChangesAsync();

                    //send order eamil
                    var emailDto = new
                    {
                        to= User.GetLoginEmail(),
                        Subject = "Order Place Successfully",
                        Body = $"Dear {User.GetLoginName()},\r\n\r\nThank you for shopping with VMart!\r\n\r\nWe are pleased to confirm that your order {orderId} has been successfully placed. Below are the details of your order:\r\n\r\nOrder Summary:\r\n\r\n1. Order Number: {orderId}\r\n2. Order Date: {DateTime.Now}"
                    };

                    _messageBus.PublishMessage(new VMart.MessageBus.Models.Dto.PublishMessageDto
                    {
                        Exchange = "Email-Exchange",
                        Queue = "Email-Provider-Queue",
                        RoutingKey = "Email-Exchange-Key",
                        Message = JsonConvert.SerializeObject(emailDto)
                    });

                    _response.Message = "Order Placed Successfully!";
                    _response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }
    }
}
