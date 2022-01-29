using DocumentBroker.Request_objects;
using DocumentBroker.Utils;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Xml;
using System.Xml.Linq;
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

            channel.QueueDeclare("Application1_Out_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueDeclare("Application2_Out_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueDeclare("Generate_Out_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueDeclare("Store_Out_queue",
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
                    XDocument doc = XDocument.Parse(message);

                    string Application = "";

                    foreach (XElement ApplicationElement in doc.Descendants("Application"))
                    {
                        Application = (string)ApplicationElement;
                    }

                    // naar welke service moet ik gaan a.h.v. requesttype
                    switch (request.ToString())
                    {
                        case "g":

                            //request message to generate
                            autoXml = se.CeateGR(message);
                            encodedAutoXml = Encoding.UTF8.GetBytes(autoXml);
                            channel.BasicPublish("", "broker_to_generate", null, encodedAutoXml);

                            //response message to aplication
                            autoXml = se.CreateGRResponse(message);
                            encodedAutoXml = Encoding.UTF8.GetBytes(autoXml);

                            if (se.IsItAplication1(Application))
                            {
                                channel.BasicPublish("", "Application1_Out_queue", null, encodedAutoXml);
                            }
                            else
                            {
                                channel.BasicPublish("", "Application2_Out_queue", null, encodedAutoXml);
                            }

                            break;

                        case "s":

                            autoXml = se.CeateSR(message);
                            encodedAutoXml = Encoding.UTF8.GetBytes(autoXml);
                            channel.BasicPublish("", "broker_to_store", null, encodedAutoXml);

                            //response message to aplication
                            autoXml = se.CreateSRResponse(message);
                            encodedAutoXml = Encoding.UTF8.GetBytes(autoXml);

                            if (se.IsItAplication1(Application))
                            {
                                channel.BasicPublish("", "Application1_Out_queue", null, encodedAutoXml);
                            }
                            else
                            {
                                channel.BasicPublish("", "Application2_Out_queue", null, encodedAutoXml);
                            }

                            break;

                        case "0":

                            autoXml = se.CeateGR(message);
                            encodedAutoXml = Encoding.UTF8.GetBytes(autoXml);
                            channel.BasicPublish("", "broker_to_generate", null, encodedAutoXml);

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
            //Console.WriteLine("Generate_In_queue Consumer started");
            //channel.BasicConsume("broker_to_store", true, consumer);
            //Console.WriteLine("Store_In_queue Consumer started");
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
            gr.documentType = GiveDocumentType(input);
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
            gr.documentType = GiveDocumentType(input);
            gr.binary = GiveBinary(input);

            var sw = new StringWriter();
            serializer.Serialize(sw, gr);

            return sw.ToString();
        }

        public string CreateGRResponse(string input)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GDRR));
            GDRR grResponse = new GDRR();

            XDocument doc = XDocument.Parse(input);

            string Con = "";

            foreach (XElement ConElement in doc.Descendants("Con"))
            {
                Con = (string)ConElement;
            }

            ContextGDRR context = new ContextGDRR();
            context.requestId = Con;
            context.status = "Succes";

            grResponse.context = context;

            var sw = new StringWriter();
            serializer.Serialize(sw, grResponse);

            return sw.ToString();
        }

        public string CreateSRResponse(string input)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SDRR));
            SDRR srResponse = new SDRR();

            XDocument doc = XDocument.Parse(input);

            string Con = "";

            foreach (XElement ConElement in doc.Descendants("Con"))
            {
                Con = (string)ConElement;
            }

            ContextSDRR context = new ContextSDRR();
            context.requestId = Con;
            context.status = "Succes";

            srResponse.context = context;

            var sw = new StringWriter();
            serializer.Serialize(sw, srResponse);

            return sw.ToString();
        }

        private XmlElement GiveGenerate(string input)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(input);

            XmlNode node = document.SelectSingleNode("/GenerateDocumentRequest/Generate/Parameters");

            return node as XmlElement;
        }

        private string GiveDocumentType(string input)
        {
            XDocument doc = XDocument.Parse(input);

            string DocumentType = "";

            foreach (XElement DocumentTypeElement in doc.Descendants("DocumentType"))
            {
                DocumentType = (string)DocumentTypeElement;
            }

            return DocumentType;
        }

        private string GiveBinary(string input)
        {
            XDocument doc = XDocument.Parse(input);

            string Binary = "";

            foreach (XElement BinaryElement in doc.Descendants("Binary"))
            {
                Binary = (string)BinaryElement;
            }

            return Binary;
        }

        public bool IsItAplication1(string input)
        {

            if (input == "F507A58E-8104-4379-964F-ABC4107983D7")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
