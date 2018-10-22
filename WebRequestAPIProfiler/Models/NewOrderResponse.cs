using System.Xml.Serialization;

namespace WebRequestAPIProfiler.Models
{
    [XmlRoot("")]
    public class NewOrderResponse
    {
        

        [XmlElement(Namespace = "")]
        public long OrderID { get; set; }

        [XmlElement(Namespace = "")]
        public long AgentOrderReference { get; set; }

        [XmlElement(Namespace = "")]
        public string RecipientFirstName { get; set; }

        [XmlElement(Namespace = "")]
        public string RecipientLastName { get; set; }
                
        [XmlElement(Namespace = "")]
        public string ResultCodes { get; set; }

        [XmlElement(Namespace = "")]
        public string SenderCompanyName { get; set; }

        [XmlElement(Namespace = "")]
        public string SenderFirstName { get; set; }

        [XmlElement(Namespace = "")]
        public string SenderLastName { get; set; }

        [XmlElement(Namespace = "")]
        public string NetAmountSent { get; set; }

        [XmlElement(Namespace = "")]
        public string AmountReceived { get; set; }

        

    }
}
