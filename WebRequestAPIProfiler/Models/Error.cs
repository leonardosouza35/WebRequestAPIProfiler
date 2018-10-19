using System.Xml.Serialization;

namespace WebRequestAPIProfiler.Models
{
    [XmlRoot(Namespace = "")]
    public class Error
    {
        [XmlElement(Namespace = "")]
        public string ResultCodes { get; set; }

        [XmlElement(Namespace = "")]
        public string ResultMessage { get; set; }
    }
}
