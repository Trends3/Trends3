using DocumentBroker;
using DocumentBroker.Queues;
using RabbitMQ.Client;

class Program
{

    static void Main(string[] args)
    {
        ConnectionFactory factory;

        factory = new ConnectionFactory
        {
            HostName = "rabbitmq",
            Port = 5672
        };
        factory.UserName = "guest";
        factory.Password = "guest";

        using var connection2 = factory.CreateConnection();
        using var channel = connection2.CreateModel();


        QueueConsumer queueConsumer = new QueueConsumer();
        QueueProducer queueProducer = new QueueProducer();


        queueConsumer.Consume(channel);

    }
}
