﻿using Newtonsoft.Json;
using System.Net;
using System.Text;
using VMart.WebApp.Models.Dto;
using VMart.WebApp.Services.IServices;
using VMart.WebApp.Utility;

namespace VMart.WebApp.Services
{
    public class RequestBase : IRequestBase
    {
        private readonly HttpClient _httpClient;
        public RequestBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseDto<T>> SendAsync<T>(RequestDto request)
        {
            var response = new ResponseDto<T>();
            try
            {
                var apiRequest = new HttpRequestMessage();
                apiRequest.RequestUri = new Uri(request.Url);
                if (request.Header != null && request.Header.Count() > 0)
                {
                    foreach (var item in request.Header)
                    {
                        apiRequest.Headers.Add(item.Key, item.Value);
                    }
                }
                if (!string.IsNullOrEmpty(request.Token))
                {
                    apiRequest.Headers.Add("Authorization", $"Bearer {request.Token}");
                }
                if (request.RequestBody != null)
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(request.RequestBody), Encoding.UTF8, "application/json");
                    apiRequest.Content = jsonContent;
                }
                switch (request.RequestType)
                {
                    case RequestType.GET:
                        apiRequest.Method = HttpMethod.Get;
                        break;
                    case RequestType.POST:
                        apiRequest.Method = HttpMethod.Post;
                        break;
                    case RequestType.PUT:
                        apiRequest.Method = HttpMethod.Put;
                        break;
                    case RequestType.DELETE:
                        apiRequest.Method = HttpMethod.Delete;
                        break;
                    default:
                        response.Message = "Unknow Request Found.";
                        break;
                }
                var client = await _httpClient.SendAsync(apiRequest);
                if (client != null)
                {
                    response.Message = client.StatusCode.ToString();
                    if (client.StatusCode == HttpStatusCode.OK)
                    {
                        var apiRes = await client.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ResponseDto<T>>(apiRes);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
