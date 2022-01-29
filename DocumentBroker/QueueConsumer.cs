using DocumentBroker.Request_objects;
using DocumentBroker.Utils;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Xml;
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

            channel.QueueDeclare("broker_to_generate",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueDeclare("broker_to_store",
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
                       
                        validator.validation("GenerateRequest_validator2.xsd", message);
                        break;
                    case "s":
                
                        validator.validation("StoreDocument_validator.xsd", message);

                        break;
                    case "0":
                
                        validator.validation("GenerateStoreRequest_validator.xsd", message);

                        break;

                    default:
                        Console.WriteLine("Is invalid");
                        break;
                }
         

                if (Validator.ValidForQ == true)
                {
                    Serialize se = new Serialize();
                    string autoXml;
                    byte[] encodedAutoXml;

                    // naar welke service moet ik gaan a.h.v. requesttype
                    switch (request.ToString())
                    {
                        case "g":

                            autoXml = se.CeateGR(message);
                            encodedAutoXml = Encoding.UTF8.GetBytes(autoXml);
                            channel.BasicPublish("", "broker_to_generate", null, encodedAutoXml);
                            break;

                        case "s":

                            autoXml = se.CeateSR(message);
                            encodedAutoXml = Encoding.UTF8.GetBytes(autoXml);
                            channel.BasicPublish("", "broker_to_store", null, encodedAutoXml);

                            break;

                        case "0":


                            break;

                        default:
                            Console.WriteLine("Message Is invalid");
                            break;
                    }
                }

            };


            channel.BasicConsume("Applications_In_queue", true, consumer);
            Console.WriteLine("Applications_In_queue Consumer started");
            //channel.BasicConsume("broker_to_generate", true, consumer);
            Console.WriteLine("Generate_In_queue Consumer started");
            //channel.BasicConsume("broker_to_store", true, consumer);
            Console.WriteLine("Store_In_queue Consumer started");
            Console.ReadLine();
        }
    }
    public class Serialize
    {

        public string CeateGR(string input)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GenerationRequest));
            GenerationRequest gr = new GenerationRequest();

            gr.Ticket = Guid.NewGuid();
            gr.documentType = GiveDocumentType(input, true);
            gr.Payload = GiveGenerate(input);

            var sw = new StringWriter();
            serializer.Serialize(sw, gr);

            return sw.ToString();
        }

        public string CeateSR(string input)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(StorageRequest));
            StorageRequest gr = new StorageRequest();

            gr.Ticket = Guid.NewGuid();
            gr.documentType = GiveDocumentType(input, false);
            gr.binary = GiveBinary(input);

            var sw = new StringWriter();
            serializer.Serialize(sw, gr);

            return sw.ToString();
        }

        public XmlElement GiveGenerate(string input)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(input);

            XmlNode node = document.SelectSingleNode("/GenerateDocumentRequest/Generate/Parameters");

            return node as XmlElement;
        }

        public string GiveDocumentType(string input, bool isGenerate)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(input);
            XmlNode node;

            if (isGenerate)
            {
                node = document.SelectSingleNode("/GenerateDocumentRequest/Document/DocumentType");
                return node.FirstChild.ToString();
            }
            else
            {
                node = document.SelectSingleNode("/StoreDocumentRequest/Document/DocumentType");
                return node.FirstChild.ToString();
            }
        }

        public string GiveBinary(string input)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(input);

            XmlNode node = document.SelectSingleNode("/StoreDocumentRequest/Document/Binary");

            return node.FirstChild.ToString();
        }

    }
}
