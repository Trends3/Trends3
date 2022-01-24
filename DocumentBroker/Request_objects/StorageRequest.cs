using System.Xml;
using System.Xml.Serialization;

namespace DocumentBroker.Request_objects
{

    [XmlRoot("StorageRequest")]
    public class StorageRequest
    {

        [XmlElement("Ticket")]
        public Guid Ticket;

        public XmlElement? DocumentType { get; set; }

        public XmlElement? Binary { get; set; }

    }
}