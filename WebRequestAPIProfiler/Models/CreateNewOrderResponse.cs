using System.Xml.Serialization;

namespace WebRequestAPIProfiler.Models
{
    [XmlRoot("CreateNewOrderResponse", Namespace = "http://mts.geobridge.org/")]
    public class CreateNewOrderResponse
    {
        [XmlElement(Namespace = "http://mts.geobridge.org/")]
        public CreateNewOrderResult CreateNewOrderResult { get; set; }

        public bool HasError
        {
            get { return CreateNewOrderResult != null 
                    && CreateNewOrderResult.Error != null 
                    && !string.IsNullOrEmpty(CreateNewOrderResult.Error.ResultCodes); }
        }

        public string ResultCodes
        {
            get { return HasError ? CreateNewOrderResult.Error.ResultCodes : string.Empty; }
        }

        public string ResultMessage
        {
            get
            {
                return HasError ? CreateNewOrderResult.Error.ResultMessage : string.Empty; 
            }
        }

        public NewOrderResponse NewOrderResponse { get { return CreateNewOrderResult.NewOrderResponse; } }

    }
}
