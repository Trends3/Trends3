using DocumentBroker;
using DocumentBroker.Queues;
using RabbitMQ.Client;
using System.Data.SqlClient;

class Program
{

    //Connection naar Rabbitmq Docker Container

    public static string URI = @"amqp://guest:guest@172.19.0.3:5672";

    static void Main(string[] args)
    {
    
        var factory = new ConnectionFactory
        {
            Uri = new Uri(URI)
        };
        //Consumer voor queue

        using var connection2 = factory.CreateConnection();
        using var channel = connection2.CreateModel();
        //QueueConsumer.Consume(channel);

        QueueConsumer queueConsumer = new QueueConsumer();
        //QueueProducer queueProducer = new QueueProducer();

        // Create all queues for the broker

        //queueProducer.ProduceQueues(channel);

        // Create The In queue for the broker

        queueConsumer.Consume(channel);

    }
}
