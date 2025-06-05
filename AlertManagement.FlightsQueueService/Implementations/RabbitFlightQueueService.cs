using AlertManagement.FlightsQueueService.Configurations;
using AlertManagement.FlightsQueueService.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace AlertManagement.FlightsQueueService.Implementations
{
    public class RabbitFlightQueueService : IFlightQueueService
    {
        private readonly FlightsQueueOptions _options;
        private readonly RabbitMQ.Client.IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;

        public RabbitFlightQueueService(string hostName = "localhost", string queueName = "flight.watchlist.in")
        {
            _queueName = queueName;

            var factory = new ConnectionFactory() { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false);
        }

        public Task AddFlightAsync(string flightNumber)
          => SendAsync("add", flightNumber);

        public Task RemoveFlightAsync(string flightNumber)
            => SendAsync("remove", flightNumber);

        private Task SendAsync(string action, string flightNumber)
        {
            var payload = new
            {
                Action = action,
                FlightNumber = flightNumber
            };

            var json = JsonSerializer.Serialize(payload);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "", routingKey: _options.QueueName, basicProperties: null, body: body);
            return Task.CompletedTask;
        }
    }
}
