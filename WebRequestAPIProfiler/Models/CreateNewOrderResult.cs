using System.Xml.Serialization;

namespace WebRequestAPIProfiler.Models
{
    [XmlRoot("CreateNewOrderResult", Namespace = "http://mts.geobridge.org/")]
    public class CreateNewOrderResult
    {
        [XmlElement(Namespace = "")]
        public NewOrderResponse NewOrderResponse { get; set; }

        [XmlElement(Namespace = "")]
        public Error Error { get; set; }
        

    }
}
