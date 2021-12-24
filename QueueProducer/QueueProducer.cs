using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace QueueProducer
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel)
        {
            XmlTextReader r = new XmlTextReader("user2.xml");

            string jsonData = @"{
            'id':'1',
            'firstName':'Ben',
            'lastname':'Benson',
            'gender':'Male',
            'nationality':'Belgian',
            'street':'Teststraat',
            'housenumber':'1',
            'zip':'1000',
            'email':'ben@benson.com'
            }";


            channel.QueueDeclare("queue1",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"{jsonData}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("", "queue1", null, body);
                count++;
                Thread.Sleep(3000);
            }
        }
    }
}