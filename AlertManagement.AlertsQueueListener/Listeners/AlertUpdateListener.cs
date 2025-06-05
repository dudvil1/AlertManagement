using AlertManagement.AlertsQueueListener.Configurations;
using AlertManagement.AlertsQueueListener.Models;
using AlertManagement.CacheService.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace AlertManagement.AlertsQueueListener.Listeners
{
    public class AlertUpdateListener : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ICacheService _cacheService;
        private const string QueueName = "alert.price-check.in";

        public AlertUpdateListener(
                   ICacheService cacheService,
                   IOptions<AlertsQueueOptions> queueOptions)
        {
            _cacheService = cacheService;
            var opts = queueOptions.Value;

            var factory = new ConnectionFactory { HostName = opts.HostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                queue: opts.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var message = JsonSerializer.Deserialize<FlightUpdateMessage>(json);
                if (message != null)
                {
                    var users = await _cacheService.GetByFlightAsync(message.FlightNumber);

                    // here in the future: call SocketService and/or PushService
                    Console.WriteLine($"[Alert Listener] Flight {message.FlightNumber}: Notifying {users.Count} users");
                }
            };

            _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}