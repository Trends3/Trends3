using System.Xml;
using System.Xml.Serialization;

namespace DocumentBroker.Request_objects
{
    [XmlRoot("StoreDocumentResponce")]
    public class SDRR
    {

        [XmlElement("Context")]
        public ContextSDRR context;

    }

    public class ContextSDRR
    {
        [XmlElement("RequestId")]
        public string requestId;

        [XmlElement("Status")]
        public string status;
    }
}
