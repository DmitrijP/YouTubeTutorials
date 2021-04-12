using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQTutorials.Producer
{
    public class RabbitClient
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName = "direct_greetings";

        public RabbitClient(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
        }

        public void SendMessage(string key, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: _exchangeName, routingKey: key, basicProperties: null, body: body);
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
