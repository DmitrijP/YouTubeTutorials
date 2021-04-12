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
        private readonly string _exchangeName = "direct_greetings";
        private string _queueName;

        public RabbitClient(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
            _queueName = _channel.QueueDeclare().QueueName;
        }

        public void BindQueueToRoutingKey(string key)
        {
            _channel.QueueBind(_queueName, _exchangeName, routingKey: key);
        }

        public void RegisterForMessageHandling()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var testMessage = Encoding.UTF8.GetString(body);
                Console.WriteLine($"RoutedToKey: {ea.RoutingKey} Message: {testMessage}" );
            };
            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
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
