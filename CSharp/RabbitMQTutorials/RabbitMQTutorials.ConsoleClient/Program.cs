using RabbitMQ.Client;
using System;

namespace RabbitMQTutorials.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press <p> for creation of producer or <c> for creation of consumer!");
            var k = Console.ReadKey();
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                VirtualHost = "/YoutubeTutorials",
                UserName = "YoutubeTutorialUser",
                Password = "123456789"
            };
            if (k.KeyChar == 'p')
                InitProducer(factory);
            if (k.KeyChar == 'c')
                InitConsumer(factory);

            Console.ReadLine();

           
        }

        private static void InitConsumer(ConnectionFactory factory)
        {
            var consumer = new Consumer.RabbitClient(factory.CreateConnection());
            Console.WriteLine("Please enter a routing key:");
            var key = Console.ReadLine();
            consumer.BindQueueToRoutingKey(key);
            consumer.RegisterForMessageHandling();
            Console.WriteLine("Consumer is online, press enter to quit");
            Console.ReadLine();
            consumer.Dispose();
        }

        private static void InitProducer(ConnectionFactory factory)
        {
            var producer = new Producer.RabbitClient(factory.CreateConnection());
            Console.WriteLine("Producer is online, enter a message or <--quit> to quit");
            string message;
            while ((message = Console.ReadLine()) != "--quit")
            {
                Console.WriteLine("Please enter a routing key:");
                var key = Console.ReadLine();
                producer.SendMessage(key, message);
                Console.WriteLine($"Message was send to {key}, enter a new message or <--quit> to quit");
            }
            producer.Dispose();
        }
    }
}
