using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQTutorials.Consumer
{
    public class RabbitClient : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitClient(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "my_first_queue", durable: false, exclusive: false, autoDelete: false);
        }

        public void RegisterForMessageHandling()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var testMessage = Encoding.UTF8.GetString(body);
                Console.WriteLine(testMessage);
            };
            _channel.BasicConsume(queue: "my_first_queue", autoAck: true, consumer: consumer);
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
