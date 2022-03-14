using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{
    public class FaultSoap
    {
        [XmlElement(ElementName ="codigo")]
        public string Codigo { get; set; }
        [XmlElement(ElementName = "descripcion")] 
        public string Descripcion { get; set; }
    }
}