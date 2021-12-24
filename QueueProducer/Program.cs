using RabbitMQ.Client;
using System;
using System.Text;
using Newtonsoft.Json;

namespace QueueProducer
{
    static class Program
    {
        static void Main(String[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            QueueProducer.Publish(channel);
        }
    }
}