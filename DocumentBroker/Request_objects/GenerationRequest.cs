using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DocumentBroker.Request_objects
{
    [XmlRoot("GenerationRequest")]
     class GenerationRequest
    {

        // ticket 
        // Payload = content van message
        // 

        [XmlElement("Ticket")]
        public Guid Ticket = Guid.NewGuid();

        [XmlElement("Payload")]
        public Payload ? payload { get; set; }

    }

    internal class Payload
    {
        // payload van queue 
        [XmlElement("Message")]
        public string message = "test";
    }


}
