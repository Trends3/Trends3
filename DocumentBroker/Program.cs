﻿using DocumentBroker;
using DocumentBroker.Utils;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    //using var connection = new SqlConnection("Data Source=database; User ID=trends3;Password=trends3");
    //  connection.Open();

//Connection naar Rabbitmq Docker Container
public static string URI = "amqp://guest:guest@172.18.0.2:5672";

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


        // document valideren ( op alle param.)

        //Validator.validation("GenerateRequest_validator2.xsd" , "GenerateDocumentRequest.xml");


        //// zoeken naar een bepaalde tag, om de request te kennen

        //if (IsValid)
        //{
        //    RequestAnalyser.SearchRequest(@"GenerateDocumentRequest.xml");
        //    RequestAnalyser.SearchRequest(@"StoreDocumentRequest.xml");
        //    RequestAnalyser.SearchRequest(@"GenerateStoreDocumentRequest.xml");
        //}
    }
}