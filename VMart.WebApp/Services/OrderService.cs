﻿using VMart.WebApp.Models;
using VMart.WebApp.Models.Dto;
using VMart.WebApp.Services.IServices;
using VMart.WebApp.Utility;

namespace VMart.WebApp.Services
{
    public class OrderService: IOrderService
    {
        private readonly IRequestBase _requestBase;
        private readonly ITokenProvider _tokenProvider;
        public OrderService(IRequestBase requestBase, ITokenProvider tokenProvider)
        {
            _requestBase = requestBase;
            _tokenProvider = tokenProvider;
        }
        public async Task<ResponseDto<object>> Place(OrderDto orderDto)
        {
            var res = await _requestBase.SendAsync<object>(new RequestDto
            {
                Url = "http://localhost:5250/gateway/api/order",
                RequestType = RequestType.POST,
                Token = await _tokenProvider.GetToken(),
                RequestBody = orderDto
            });
            return res;
        }
        public async Task<ResponseDto<List<Orders>>> GetAsync()
        {
            var res = await _requestBase.SendAsync<List<Orders>>(new RequestDto
            {
                Url = "http://localhost:5250/gateway/api/order",
                RequestType = RequestType.GET,
                Token = await _tokenProvider.GetToken()
            });
            return res;
        }
        public async Task<ResponseDto<List<OrderDetails>>> GetDetailsAsync(int orderId)
        {
            var res = await _requestBase.SendAsync<List<OrderDetails>>(new RequestDto
            {
                Url = $"http://localhost:5250/gateway/api/order/{orderId}",
                RequestType = RequestType.GET,
                Token = await _tokenProvider.GetToken()
            });
            return res;
        }
    }
}
