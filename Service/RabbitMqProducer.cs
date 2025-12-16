using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace FoodNet.Service;

public class RabbitMqProducer : IMessageProducer
{
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost" 
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "product_created_queue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: "",
            routingKey: "product_created_queue",
            basicProperties: null,
            body: body);
    }
}