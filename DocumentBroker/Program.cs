using DocumentBroker;
using RabbitMQ.Client;

class Program
{
    //using var connection = new SqlConnection("Data Source=database; User ID=trends3;Password=trends3");
    //  connection.Open();

    //Connection naar Rabbitmq Docker Container
    public static string URI = @"amqp://guest:guest@172.18.0.3:5672";

    static void Main(string[] args)
    {

        var factory = new ConnectionFactory
        {
            Uri = new Uri(URI)
        };
        //Consumer voor queue
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        //QueueConsumer.Consume(channel);

        QueueConsumer queueConsumer = new QueueConsumer();

        queueConsumer.Consume(channel);



    }
}