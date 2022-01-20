using DocumentBroker;
using DocumentBroker.Queues;
using DocumentBroker.Request_objects;
using RabbitMQ.Client;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;

class Program
{

//Connection naar Rabbitmq Docker Container

public static string URI = @"amqp://guest:guest@172.18.0.2:5672";

    static void Main(string[] args)
    {
        SqlConnection connection = new SqlConnection("Data Source=Documentbroker; User ID=SA;Password=YourStrong@Passw0rd");
        SqlCommand nonqueryCommand = connection.CreateCommand();
        try
        {
            connection.Open();

            nonqueryCommand.ExecuteNonQuery();
            Console.WriteLine("Database created, now switching");
            connection.ChangeDatabase("Documentbroker");

            nonqueryCommand.CommandText = "CREATE TABLE Documentbrokers (" +
                "Id INT PRIMARY KEY NOT NULL," +
                "TicketId VARCHAR(100)," +
                "ApplicationId VARCHAR(100)," +
                "RequestType CHAR(1)" +
                ")";
            Console.WriteLine(nonqueryCommand.CommandText);
            Console.WriteLine("Number of Rows Affected is: {0}", nonqueryCommand.ExecuteNonQuery());


        }
        catch (SqlException ex)
        {

            Console.WriteLine(ex.ToString());

        }
        finally
        {

            connection.Close();
            Console.WriteLine("Connection Closed.");

        }

        var factory = new ConnectionFactory
        {
            Uri = new Uri(URI)
        };
        //Consumer voor queue
        
        using var connection2 = factory.CreateConnection();
        using var channel = connection2.CreateModel();
        //QueueConsumer.Consume(channel);

        QueueConsumer queueConsumer = new QueueConsumer();
        QueueProducer queueProducer = new QueueProducer();

        // Create all queues for the broker

        queueProducer.ProduceQueues(channel);

        // Create The In queue for the broker

        queueConsumer.Consume(channel);


        
        
    }

    // public void CreatePO(string filename)
    //{
    //    XmlSerializer serializer =
    //    new XmlSerializer(typeof(GenerationRequest));
    //    TextWriter writer = new StreamWriter(filename);
    //    GenerationRequest po = new GenerationRequest();

    //    serializer.Serialize(writer, po);
    //    writer.Close();
    //}

    //public void ReadPO(string filename)
    //{
    //    XmlSerializer serializer = new XmlSerializer(typeof(GenerationRequest));
   
    //    FileStream fs = new FileStream(filename, FileMode.Open);
    //    GenerationRequest po;
    //    po = (GenerationRequest)serializer.Deserialize(fs);

    //    Console.WriteLine(po);
    //}
}
