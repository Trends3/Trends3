using System.Xml;
using System.Xml.Serialization;

namespace DocumentBroker.Request_objects
{
    [XmlRoot("StorageRequest")]
    public class StorageRequest
    {

        [XmlElement("Ticket")]
        public Guid Ticket;

        [XmlElement("DocumentType")]
        public string? documentType;

        [XmlElement("Binary")]
        public string? binary;

    }
}