using Newtonsoft.Json;
using System.Xml.Serialization;

namespace cmes_webapi.Api.Dto
{
    [XmlRoot(ElementName = "ResponseResultDto")]
    public class ResponseResultDto<T>
    {
        [XmlElement(ElementName = "BGBAResultadoOperacion")]
        public BGBAResultadoOperacionDto BGBAResultadoOperacion { get; set; }

        [XmlElement(ElementName = "BGBAResultadoOperacionLog")]
        public BGBAResultadoOperacionLogDto BGBAResultadoOperacionLog { get; set; }

        [XmlElement(ElementName = "Datos")]
        public T Datos { get; set; }
      
    }
}
