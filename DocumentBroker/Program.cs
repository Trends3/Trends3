using DocumentBroker;
using DocumentBroker.Queues;
using RabbitMQ.Client;

class Program
{

    //Connection naar Rabbitmq Docker Container

    public static string URI = @"amqp://guest:guest@172.18.0.3:5672";

    static void Main(string[] args)
    {

        var factory = new ConnectionFactory
        {
            Uri = new Uri(URI)
        };


        using var connection2 = factory.CreateConnection();
        using var channel = connection2.CreateModel();
      

        QueueConsumer queueConsumer = new QueueConsumer();
        QueueProducer queueProducer = new QueueProducer();


        queueConsumer.Consume(channel);

    }
}
