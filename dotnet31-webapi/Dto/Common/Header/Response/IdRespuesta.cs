using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{
    
    [XmlRoot(ElementName = "idRespuesta")]
    public class IdRespuesta
    {
        [XmlElement(ElementName = "nombreProveedor")]
        public string NombreProveedor { get; set; }
    }
}
