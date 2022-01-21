using DocumentBroker.Request_objects;
using DocumentBroker.Request_objects;
using DocumentBroker.Utils;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Xml.Serialization;

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

                            // een object moeten aanmaken ( request naar juiste service xml ) 
                            // we sturen die xml op de queue van de service 

                            break;

                        case "s":

                            // een object moeten aanmaken ( request naar juiste service xml ) 
                            // we sturen die xml op de queue van de service

                            break;

                        case "0":

                            // een object moeten aanmaken ( request naar juiste service xml ) 
                            // we sturen die xml op de queue van de service

                            break;

                        default:
                            Console.WriteLine("Message Is invalid");
                            break;
                    }
                }

            };
            Serialize se = new Serialize();
            se.CeateGR();
            Console.WriteLine(se.ToString());

            channel.BasicConsume("Applications_In_queue", true, consumer);
            Console.WriteLine("Applications_In_queue Consumer started");
            channel.BasicConsume("Generate_In_queue", true, consumer);
            Console.WriteLine("Generate_In_queue Consumer started");
            channel.BasicConsume("Store_In_queue", true, consumer);
            Console.WriteLine("Store_In_queue Consumer started");
            Console.ReadLine();
        }
    }
    public class Serialize
    {
        public string CeateGR()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GenerationRequest));
            GenerationRequest gr = new GenerationRequest();

            gr.Ticket = Guid.NewGuid();

            Payload payload = new Payload();
            payload.message = "Generation Request Test";

            gr.payload = payload;
            var sw = new StringWriter();
            serializer.Serialize(sw, gr);
            return sw.ToString();
        }
    }
}
