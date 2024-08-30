using VMart.Services.EmailAPI.Middleware;

namespace VMart.Services.EmailAPI.Extentions
{
    public static class RabbitMQConsumer
    {
        public static IApplicationBuilder UseRabbitMQConsumer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RabbitMQConsumerMiddleware>();
        }
    }
}
