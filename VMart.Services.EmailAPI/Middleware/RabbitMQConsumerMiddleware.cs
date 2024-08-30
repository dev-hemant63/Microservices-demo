using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using VMart.Services.EmailAPI.Models.DTOs;
using VMart.Services.EmailAPI.Services.IService;

namespace VMart.Services.EmailAPI.Middleware
{
    public class RabbitMQConsumerMiddleware
    {
        private readonly RequestDelegate _next;
        private IConnection _connection;
        private IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        public RabbitMQConsumerMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            InitializeRabbitMQ();
            _serviceProvider = serviceProvider;
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "Email-Provider-Queue",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            StartConsuming();
        }

        private void StartConsuming()
        {
            bool autoAck = false;
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var emailDto = JsonConvert.DeserializeObject<SendDto>(message); 
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _emailService = scope.ServiceProvider.GetRequiredService<IEmailProvider>();
                    var res = _emailService.send(emailDto).Result;
                    autoAck = res.IsSuccess;
                }
            };

            _channel.BasicConsume(queue: "Email-Provider-Queue",
                                 autoAck: autoAck,
                                 consumer: consumer);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
