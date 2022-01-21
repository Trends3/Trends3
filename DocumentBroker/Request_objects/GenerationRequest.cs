using System.Xml.Serialization;

namespace DocumentBroker.Request_objects
{
    [XmlRoot("GenerationRequest")]
    public class GenerationRequest
    {

        // ticket 
        // Payload = content van message
        // 

        [XmlElement("Ticket")]
        public Guid Ticket;

        [XmlElement("Payload")]
        public Payload? payload;

    }

    public class Payload
    {
        // payload van queue 
        [XmlElement("Message")]
        public string message;
    }

}
