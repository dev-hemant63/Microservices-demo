using VMart.MessageBus.Models.Dto;

namespace VMart.MessageBus.Services.IService
{
    public interface IMessageBus
    {
        Task<ResponseDto> PublishMessage(PublishMessageDto publishMessage);
        Task<ResponseDto> ConsumeMessage(ConsumeMessageDto consumeMessageDto);
    }
}
