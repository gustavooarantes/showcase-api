using System.Text;
using System.Text.Json;
using FoodNet.DTO;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FoodNet.BackgroundServices;

public class ProductCreatedWorker : BackgroundService
{
    private readonly ILogger<ProductCreatedWorker> _logger;
    private IConnection _connection;
    private IModel _channel;

    public ProductCreatedWorker(ILogger<ProductCreatedWorker> logger)
    {
        _logger = logger;
        InitializeRabbitMq();
    }

    private void InitializeRabbitMq()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "product_created_queue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            var productDto = JsonSerializer.Deserialize<ProductResponseDto>(content);

            _logger.LogInformation($"[Background Job] New Product Received: {productDto?.Name} | Price: {productDto?.Price}");
            
        };

        _channel.BasicConsume(queue: "product_created_queue", autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }
    
    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}