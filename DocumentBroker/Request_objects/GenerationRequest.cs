using System.Xml;
using System.Xml.Serialization;

namespace DocumentBroker.Request_objects
{
    [XmlRoot("GenerationRequest")]
    public class GenerationRequest
    {

        [XmlElement("Ticket")]
        public Guid Ticket;

        [XmlElement("DocumentType")]
        public string documentType;

        public XmlElement? Payload { get; set; }

    }
}
