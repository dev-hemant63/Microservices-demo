using VMart.MessageBus.Models.Dto;

namespace VMart.MessageBus.Services.IService
{
    public interface IRabbitMQService
    {
        Task<ResponseDto> PublishMessage(PublishMessageDto publishMessage);
        Task<ResponseDto> ConsumeMessage(ConsumeMessageDto consumeMessageDto);
    }
}
