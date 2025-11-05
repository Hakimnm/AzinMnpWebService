using System.Xml.Serialization;

namespace AzinMnpWebService.Models.Response;

public class SoapResponse
{
 

    [XmlRoot(ElementName="ProcessMessageResult")]
    public class ProcessMessageResult { 

        [XmlElement(ElementName="StatusCode")] 
        public int StatusCode { get; set; } 

        [XmlElement(ElementName="StatusMessage")] 
        public string StatusMessage { get; set; } 
    }

    [XmlRoot(ElementName="ProcessMessageResponse")]
    public class ProcessMessageResponse { 

        [XmlElement(ElementName="ProcessMessageResult")] 
        public ProcessMessageResult ProcessMessageResult { get; set; } 

    }

    [XmlRoot(ElementName="Body")]
    public class Body { 

        [XmlElement(ElementName="ProcessMessageResponse")] 
        public ProcessMessageResponse ProcessMessageResponse { get; set; } 
    }

    [XmlRoot(ElementName="Envelope")]
    public class Envelope { 

        [XmlElement(ElementName="Body")] 
        public Body Body { get; set; } 

    }


}