using VMart.MessageBus.Models.Dto;
using VMart.MessageBus.Services.IService;

namespace VMart.MessageBus.Services
{
    public class MessageBus: IMessageBus
    {
        public readonly IRabbitMQService _rabbitMQService;
        public MessageBus(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        public async Task<ResponseDto> ConsumeMessage(ConsumeMessageDto consumeMessageDto)
        {
            return await _rabbitMQService.ConsumeMessage(consumeMessageDto);
        }

        public async Task<ResponseDto> PublishMessage(PublishMessageDto publishMessage)
        {
            return await _rabbitMQService.PublishMessage(publishMessage);
        }
    }
}
