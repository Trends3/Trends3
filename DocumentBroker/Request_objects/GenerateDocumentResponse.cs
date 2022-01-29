using System.Xml;
using System.Xml.Serialization;

namespace DocumentBroker.Request_objects
{
    [XmlRoot("GenerateDocumentResponse")]
    public class GDRR
    {

        [XmlElement("Context")]
        public ContextGDRR context;

    }

    public class ContextGDRR
    {
        [XmlElement("RequestId")]
        public string requestId;

        [XmlElement("Status")]
        public string status;
    }
}
