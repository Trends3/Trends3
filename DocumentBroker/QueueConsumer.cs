﻿using DocumentBroker.Utils;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DocumentBroker
{
    public class QueueConsumer
    {

        private Validator validator = new Validator();
        private RequestAnalyser requestAnalyser = new RequestAnalyser();

        public void Consume(IModel channel)
        {
            //Queue maken en consumen (luisteren of er berichten op de queue komen en bekijken)

            channel.QueueDeclare("Applications_In_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueDeclare("Generate_In_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueDeclare("Store_In_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {

                //message tonen die op queue binnenkomt
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var testMessage = "";
                char request = requestAnalyser.SearchRequest(message);

                switch (request.ToString())
                {
                    case "g":
                        Console.WriteLine("Generate");
                        validator.validation("GenerateRequest_validator2.xsd", message);
                        break;
                    case "s":
                        Console.WriteLine("Store");
                        validator.validation("StoreDocument_validator.xsd", message);

                        break;
                    case "0":
                        Console.WriteLine("Generate Store");
                        validator.validation("GenerateStoreRequest_validator.xsd", message);

                        break;

                    default:
                        Console.WriteLine("Is invalid");
                        break;
                }
                //validator.validation("StoreDocument_validator.xsd", message);

                if (Validator.ValidForQ == true)
                {
                    Console.WriteLine(message);

                    // naar welke service moet ik gaan a.h.v. requesttype
                    switch (request.ToString())
                    {
                        case "g":
                            testMessage = "test1";
                            var body2 = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(testMessage));
                            channel.BasicPublish("", "Generate_Out_queue", null, body2);
                            Console.WriteLine("Generate_Out_queue: " + testMessage);
                            break;

                        case "s":
                            testMessage = "test2";
                            var body3 = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(testMessage));
                            channel.BasicPublish("", "Store_Out_queue", null, body3);
                            Console.WriteLine("Store_Out_queue: " + testMessage);

                            break;

                        case "0":
                            testMessage = "test3";
                            var body4 = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(testMessage));
                            channel.BasicPublish("", "Generate_Out_queue", null, body4);
                            channel.BasicPublish("", "Store_Out_queue", null, body4);
                            Console.WriteLine("Generate_Out_queue: " + testMessage);
                            Console.WriteLine("Store_Out_queue: " + testMessage);

                            break;

                        default:
                            Console.WriteLine("Message Is invalid");
                            break;
                    }
                }

            };
            channel.BasicConsume("Applications_In_queue", true, consumer);
            Console.WriteLine("Applications_In_queue Consumer started");
            channel.BasicConsume("Generate_In_queue", true, consumer);
            Console.WriteLine("Generate_In_queue Consumer started");
            channel.BasicConsume("Store_In_queue", true, consumer);
            Console.WriteLine("Store_In_queue Consumer started");
            Console.ReadLine();
        }
    }
}
