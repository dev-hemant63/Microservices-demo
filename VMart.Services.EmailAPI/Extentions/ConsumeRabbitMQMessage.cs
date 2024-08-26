using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using VMart.Services.EmailAPI.Models.DTOs;
using VMart.Services.EmailAPI.Services.IService;

namespace VMart.Services.EmailAPI.Extentions
{
    public static class ConsumeRabbitMQMessage
    {
        public static IApplicationBuilder UseConsumeRabbitMQMessage(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;

            try
            {
                bool autoAck = false;
                var connectionFactory = new ConnectionFactory
                {
                    HostName = "localhost",
                };
                using var connection = connectionFactory.CreateConnection();
                using var channel = connection.CreateModel();

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (sender, e) =>
                {
                    using var scope = serviceProvider.CreateScope();
                    var emailProvider = scope.ServiceProvider.GetRequiredService<IEmailProvider>();

                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var emailDto = JsonConvert.DeserializeObject<SendDto>(message);

                    var emailRes = await emailProvider.send(emailDto);
                    autoAck = emailRes.IsSuccess;
                };

                channel.BasicConsume(queue: "Email-Provider-Queue", autoAck: autoAck, consumer: consumer);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
            }

            return app;
        }
    }
}
