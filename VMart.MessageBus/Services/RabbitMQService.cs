using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using VMart.MessageBus.Models.Dto;
using VMart.MessageBus.Services.IService;

namespace VMart.MessageBus.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly ResponseDto _response;
        public async Task<ResponseDto> PublishMessage(PublishMessageDto publishMessage)
        {
            try
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = "localhost",
                };
                var connection = connectionFactory.CreateConnection();
                var chanal = connection.CreateModel();

                var messageByts = Encoding.UTF8.GetBytes(publishMessage.Message);
                chanal.BasicPublish(exchange: publishMessage.Exchange, publishMessage.RoutingKey, body: messageByts);
                _response.IsSuccess = true;
                _response.Message = "Message send successfully!";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }
        public async Task<ResponseDto> ConsumeMessage(ConsumeMessageDto consumeMessageDto)
        {
            try
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = "localhost",
                };
                var connection = connectionFactory.CreateConnection();
                var chanal = connection.CreateModel();

                var consumer = new EventingBasicConsumer(chanal);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                };
                chanal.BasicConsume(queue: consumeMessageDto.Queue, true, consumer: consumer);
                _response.IsSuccess = true;
                _response.Message = "Message consume successfully!";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
