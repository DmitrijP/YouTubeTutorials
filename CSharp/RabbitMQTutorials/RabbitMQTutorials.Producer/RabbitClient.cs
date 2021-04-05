using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQTutorials.Producer
{
    public class RabbitClient
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitClient(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "my_first_queue", durable: false, exclusive: false, autoDelete: false);
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "my_first_queue", basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
